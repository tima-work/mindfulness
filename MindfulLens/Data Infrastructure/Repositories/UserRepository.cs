using Dapper;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class UserRepository : ConnectionRepository, IUserRepository
    {

        public UserRepository()
        {

        }

        public void BanUser(User user)
        {
            DoSimpleAction("UPDATE Users SET IsBanned = 1 WHERE ID = @ID", user);
        }

        public User[] GetAllUsers()
        {
            return GetSomeUsers("SELECT * FROM Users");
        }

        private User[] GetSomeUsers(string sqlQuery, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                        }
                    }

                    List<User> users = new List<User>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User(
                                Convert.ToInt32(reader["ID"]),
                                reader["Name"].ToString(),
                                reader["Email"].ToString(),
                                reader["HashedPassword"].ToString(),
                                reader["Salt"].ToString(),
                                reader["Username"].ToString(),
                                reader["Image"] as byte[],
                                Convert.ToBoolean(reader["IsBanned"]),
                                (Importancy)Convert.ToInt32(reader["Importancy"])
                                ));
                        }
                        return users.ToArray();
                    }
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        } 

        public User? GetUserByEmail(string email)
        {
            return GetOneUser("SELECT * FROM Users WHERE Email = @Email;", new { Email = email });
        }

        public User? GetUserByID(int id)
        {
            return GetOneUser("SELECT * FROM Users WHERE ID = @ID;", new { ID = id });
        }

        public User? GetUserByUsername(string username)
        {
            return GetOneUser("SELECT * FROM Users WHERE Username = @Username;", new { Username = username });
        }

        public User? RegisterUser(User user)
        {
            DoSimpleAction("INSERT INTO Users (Name, Email, Username, HashedPassword, Salt, Importancy, IsBanned, Image) VALUES (@Name, @Email, @Username, @HashedPassword, @Salt, @Importancy, @Banned, @Photo);", user);
            return GetUserByEmail(user.Email);
            //using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            //{
            //    connection.Execute("INSERT INTO Users (Name, Email, Username, HashedPassword, Salt, Importancy, IsBanned, Image) VALUES (@Name, @Email, @Username, @HashedPassword, @Salt, @Importancy, @IsBanned, @Image);", user);
            //}
        }

        public void UpdateUser(User user)
        {
            DoSimpleAction("UPDATE Users SET Name = @Name, Email = @Email, Username = @Username, Image = @Photo WHERE ID = @ID;", user);
            //using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            //{
            //    connection.Execute("UPDATE Users SET Name = @Name, Email = @Email, Username = @Username, Photo = @Photo", user);
            //}
        }

        public void ResetPassword(User user)
        {
            DoSimpleAction("UPDATE Users SET Salt = @Salt, HashedPassword = @HashedPassword WHERE ID = @ID;", user);
        }

        private User? GetOneUser(string sqlQuery, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                        }
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User(
                                Convert.ToInt32(reader["ID"]),
                                reader["Name"].ToString(),
                                reader["Email"].ToString(),
                                reader["HashedPassword"].ToString(),
                                reader["Salt"].ToString(),
                                reader["Username"].ToString(),
                                reader["Image"] as byte[],
                                Convert.ToBoolean(reader["IsBanned"]),
                                (Importancy)Convert.ToInt32(reader["Importancy"])
                                );
                        }
                    }

                    return null;
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }

        public Dictionary<int, User> GetUserDictionary(string sqlQuery, bool checkNullability, object parameters = null)
        {
            try
            {
                Dictionary<int, User> users = new Dictionary<int, User>();
                using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            command.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                        }
                    }


                    StringBuilder stringBuilder = new StringBuilder("SELECT * FROM Users WHERE ");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (checkNullability)
                        {
                            while (reader.Read())
                            {
                                object? id = reader["CreatorID"];
                                if (id == DBNull.Value)
                                    continue;
                                stringBuilder.Append("ID = " + Convert.ToInt32(id).ToString() + " OR ");
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                stringBuilder.Append("ID = " + Convert.ToInt32(reader["CreatorID"]).ToString() + " OR ");
                            }
                        }
                    }
                    stringBuilder.Append("0 = 1");

                    SqlCommand cmd = new SqlCommand(stringBuilder.ToString(), connection);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int id;
                        User user;

                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["ID"]);
                            if (users.ContainsKey(id))
                                continue;
                            user = new User(
                                id,
                                reader["Name"].ToString(),
                                reader["Email"].ToString(),
                                reader["HashedPassword"].ToString(),
                                reader["Salt"].ToString(),
                                reader["Username"].ToString(),
                                reader["Image"] as byte[],
                                Convert.ToBoolean(reader["IsBanned"]),
                                (Importancy)Convert.ToInt32(reader["Importancy"])
                            );

                            users.Add(id, user);
                        }
                    }
                }
                return users;
            }
            catch
            {
                throw new DatabaseException();
            }
        }


        public Dictionary<int, User> GetUserDictionaryForUser(string sqlQuery, object parameters = null)
        {
            return GetUserDictionary($@"SELECT UserID FROM {sqlQuery}", false, parameters);
            //return GetUserDictionary($@"SELECT Users.ID, Users.Name, Users.Email, Users.Username, Users.HashedPassword, Users.Salt, Users.Importancy, Users.IsBanned, Users.Image
            //                            FROM Users
            //                            WHERE EXISTS (
            //                            SELECT TOP 4 * FROM Exercises WHERE Exercises.IsOfficial = 1 AND Exercises.CreatorID = Users.ID
            //                            ) AS Result ON Users.ID = Result.UserID;", parameters);
        }

        public Dictionary<int, User> GetUserDictionaryForCreator(string sqlQuery, object parameters = null)
        {
            return GetUserDictionary($@"SELECT CreatorID FROM {sqlQuery}", false, parameters);
            //return GetUserDictionary($@"SELECT Users.ID, Users.Name, Users.Email, Users.Username, Users.HashedPassword, Users.Salt, Users.Importancy, Users.IsBanned, Users.Image FROM Users WHERE EXISTS ( SELECT TOP @Number * FROM Exercises WHERE Exercises.IsOfficial = 1 AND Exercises.CreatorID = Users.ID);", parameters);
        }

        public Dictionary<int, User> GetUserDictionaryForNullableCreator(string sqlQuery, object parameters = null)
        {
            return GetUserDictionary($@"SELECT CreatorID FROM {sqlQuery}", true, parameters);
            //return GetUserDictionary($@"SELECT Users.ID, Users.Name, Users.Email, Users.Username, Users.HashedPassword, Users.Salt, Users.Importancy, Users.IsBanned, Users.Image FROM Users WHERE EXISTS ( SELECT TOP @Number * FROM Exercises WHERE Exercises.IsOfficial = 1 AND Exercises.CreatorID = Users.ID);", parameters);
        }



        public void MakeAdmin(User user)
        {
            DoSimpleAction("UPDATE Users SET Importancy = @Importancy WHERE ID = @ID", new { user.Importancy, user.Id });
        }

        public User[] GetBannedUsers()
        {
            return GetSomeUsers("SELECT * FROM Users WHERE IsBanned = 1");
        }

        public User[] GetNotBannedUsers()
        {
            return GetSomeUsers("SELECT * FROM Users WHERE IsBanned = 0");
        }

        public Dictionary<User, int> GetAuthorsOfWatchedPublicationsByUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT Users.* 
                                                 FROM Users
                                                 INNER JOIN ForumPublications
                                                 ON ForumPublications.CreatorID = Users.ID
                                                 INNER JOIN WatchedPublications
                                                 ON WatchedPublications.WatchedPublicationID = ForumPublications.ID
                                                 WHERE WatchedPublications.UserID = @UserID
                                                 AND Users.ID <> UserID", connection);
                    cmd.Parameters.AddWithValue("@UserID", user.Id);

                    Dictionary<User, int> dict_to_return = new Dictionary<User, int>();
                    int id;
                    User? found_user;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["ID"]);
                            found_user = dict_to_return.Keys.FirstOrDefault(u => u.Id == id);
                            if (found_user != null)
                                dict_to_return[found_user]++;
                            else
                            {
                                found_user = new User(
                                    id,
                                    reader["Name"].ToString(),
                                    reader["Email"].ToString(),
                                    reader["HashedPassword"].ToString(),
                                    reader["Salt"].ToString(),
                                    reader["Username"].ToString(),
                                    reader["Image"] as byte[],
                                    Convert.ToBoolean(reader["IsBanned"]),
                                    (Importancy)Convert.ToInt32(reader["Importancy"])
                                );
                                dict_to_return[found_user] = 1;
                            }
                        }
                    }

                    return dict_to_return;
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }
    }
}
