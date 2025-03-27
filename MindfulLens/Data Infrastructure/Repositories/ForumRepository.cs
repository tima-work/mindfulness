using Dapper;
using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class ForumRepository : ConnectionRepository, IForumRepository
    {
        private UserRepository userRepository;

        public ForumRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public int AddForumPublication(ForumPublication forumPublication)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO ForumPublications (CreationDate, Text, CreatorID, QuestionPublicationID, IsDeleted) OUTPUT INSERTED.ID VALUES (@CreationDate, @Text, @CreatorID, @QuestionPublicationID, @IsDeleted);", connection);
                    command.Parameters.AddWithValue("@CreationDate", forumPublication.CreationDate);
                    command.Parameters.AddWithValue("@Text", forumPublication.Text);
                    command.Parameters.AddWithValue("@CreatorID", forumPublication.Creator.Id);
                    if (forumPublication.QuestionPublication == null)
                        command.Parameters.AddWithValue("@QuestionPublicationId", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@QuestionPublicationId", forumPublication.QuestionPublication.ID);
                    command.Parameters.AddWithValue("@IsDeleted", Convert.ToInt32(forumPublication.IsDeleted));

                    return Convert.ToInt32(command.ExecuteScalar());

                }
            }
            catch
            {
                throw new DatabaseException();
            }
            //DoSimpleAction("INSERT INTO ForumPublications (CreationDate, Text, CreatorID, QuestionPublicationID, IsDeleted) VALUES (@CreationDate, @Text, @CreatorID, @QuestionPublicationID, @IsDeleted);", new { CreationDate = forumPublication.CreationDate, Text = forumPublication.Text, CreatorID = forumPublication.Creator.Id, QuestionPublicationID = forumPublication.QuestionPublication.ID, IsDeleted = forumPublication.IsDeleted });
        }

        public void DeleteForumPublication(ForumPublication forumPublication)
        {
            DoSimpleAction("UPDATE ForumPublications SET IsDeleted = 1 WHERE ID = @ID", forumPublication);
        }

        public ForumPublication[] GetForumPublicationsByCreator(User creator)
        {
            string sqlQuery = "ForumPublications WHERE CreatorID = @CreatorID;";
            object parameters = new { CreatorId = creator.Id };
            return InitConverterAndQueryDatabase(sqlQuery, parameters);
        }

        public ForumPublicationWithReplies[] GetQuestionForumPublications(string? search_query, int number)
        {
            if (search_query != null)
                return GetForumPublicationsWithReplies("ForumPublications WHERE QuestionPublicationID IS NULL AND Text LIKE '%' + @Query + '%'", number, new { Number = number, Query = search_query });
            return GetForumPublicationsWithReplies("ForumPublications WHERE QuestionPublicationID IS NULL", number, new { Number = number });
        }

        //public ForumPublication[] GetAllForumPublications()
        //{
        //    string sqlQuery = "SELECT * FROM ForumPublications;";
        //    object parameters = null;
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public void UpdateForumPublication(ForumPublication forumPublication)
        {
            DoSimpleAction("UPDATE ForumPublications SET Text = @Text WHERE ID = @ID", forumPublication);
        }

        private ForumPublication[] CastToForumPublication(Publication[] publications)
        {
            ForumPublication[] forumPublications = new ForumPublication[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                forumPublications[i] = (ForumPublication)publications[i];
            return forumPublications;
        }

        public ForumPublication? GetForumPublicationById(int id)
        {
            string sqlQuery = "ForumPublications WHERE ID = @ID;";
            object parameters = new { ID = id };
            //string questionQuery = "SELECT QuestionPublicationID FROM ForumPublication WHERE "

            string someSql = @$"SELECT fp1.CreatorID 
                                FROM ForumPublications fp1 
                                WHERE fp1.ID = @ID 
                                OR fp1.ID = 
                                    (SELECT fp2.QuestionPublicationID 
                                    FROM ForumPublications fp2 
                                    WHERE fp2.ID = {id});";



            //string userSql = @$"SELECT 
            //                    fp1.CreatorId, fp2.CreatorId
            //                    FROM ForumPublications fp1
            //                    LEFT JOIN ForumPublications fp2 
            //                    ON fp1.QuestionPublicationId = fp2.Id
            //                    WHERE fp1.Id = @ID;";
            Dictionary<int, User> users = userRepository.GetUserDictionary(someSql, false, new { ID = id });

            ForumConverter converter = new ForumConverter(users);


            ForumPublication[] casted_array = CastToForumPublication(GetData("SELECT * FROM " + sqlQuery, converter, parameters));
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        public Dictionary<int, Publication> GetForumPublicationsDictionary(string sqlQuery, object parameters)
        {
            //string sqlQuery = "SELECT * FROM ForumPublications;";
            //object parameters = null;
            ForumConverter converter = new ForumConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return GetDictionary(sqlQuery, converter, parameters);
        }

        public void AddKeywords(Keyword[] keywords, ForumPublication forumPublication)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (Keyword keyword in keywords)
                    connection.Execute("INSERT INTO ForumKeywords (ForumPublicationID, Keyword, Ranking) VALUES (@ForumPublicationID, @Keyword, @Ranking);", new { ForumPublicationID = forumPublication.ID, Keyword = keyword.keyWord, Ranking = keyword.Ranking });
            }
        }

        public void DeleteKeywordsByForumPublication(ForumPublication forumPublication)
        {
            DoSimpleAction("DELETE FROM ForumKeywords WHERE ForumPublicationID = @ForumPublicationID;", new { ForumPublicationID = forumPublication.ID });
        }

        public Dictionary<string, decimal> GetKeywordsByUser(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT ForumKeywords.Keyword, SUM(ForumKeywords.Ranking) AS TotalRanking 
                                                  FROM ForumKeywords
                                                  INNER JOIN ForumPublications
                                                  ON ForumKeywords.ForumPublicationID = ForumPublications.ID
                                                  WHERE ForumPublications.CreatorID = @CreatorID
                                                  GROUP BY ForumKeywords.Keyword", connection);

                    cmd.Parameters.AddWithValue("@CreatorID", user.Id);
                    Dictionary<string, decimal> keywords = new Dictionary<string, decimal>();
                    //List<Keyword> keywords = new List<Keyword>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            keywords.Add(reader["Keyword"].ToString(), Convert.ToDecimal(reader["TotalRanking"]));
                            //keywords.Add(new Keyword(
                            //    reader["Keyword"].ToString(),
                            //    Convert.ToInt32(reader["Ranking"])
                            //    ));
                        }
                        //return keywords.ToArray();
                        return keywords;
                    }
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }

        public void AddPublicationToWatchHistory(User user, ForumPublication forumPublication)
        {
            DoSimpleAction(@$"INSERT INTO WatchedPublications (WatchedPublicationID, UserID)
                              SELECT @WatchedPublicationID, @UserID
                              WHERE NOT EXISTS (
                                  SELECT 1 
                                  FROM WatchedPublications
                                  WHERE UserID = {user.Id}
                                  AND WatchedPublicationID = {forumPublication.ID}
                              );", new { WatchedPublicationID = forumPublication.ID, UserID = user.Id });
        }

        private ForumPublication[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            ForumConverter converter = new ForumConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToForumPublication(GetData("SELECT * FROM " + sqlQuery, converter, parameters));
        }

        public ForumPublication[] GetBranch(ForumPublication questionPublication, int number)
        {
            //List<int> past_list = new List<int>();
            StringBuilder pastStringBuilder = new StringBuilder("ForumPublications WHERE ");
            List<int> current_list = new List<int>() { questionPublication.ID };
            StringBuilder currentStringBuilder;
            List<ForumPublication> forumPublications;
            //List<int> future_list = new List<int>();
            ForumConverter converter;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    bool exit_loop = false;
                    int counter = 0;

                    while (current_list.Count > 0 && !exit_loop)
                    {
                        using (SqlConnection connection2 = new SqlConnection(connectionString))
                        {
                            connection2.Open();
                            currentStringBuilder = new StringBuilder("SELECT ID FROM ForumPublications WHERE ");
                            int length = current_list.Count;
                            for (int i = 0; i < length - 1; i++)
                            {
                                currentStringBuilder.Append($"QuestionPublicationID = {current_list[i]} OR ");
                                pastStringBuilder.Append($"ID = {current_list[i]} OR ");
                                counter++;
                                if (counter == number)
                                {
                                    exit_loop = true;
                                    break;
                                }
                            }
                            currentStringBuilder.Append($"QuestionPublicationID = {current_list[length - 1]};");
                            pastStringBuilder.Append($"ID = {current_list[length - 1]} OR ");
                            counter++;
                            if (counter == number)
                                exit_loop = true;
                            current_list.Clear();

                            SqlCommand cmd2 = new SqlCommand(currentStringBuilder.ToString(), connection2);

                            using (SqlDataReader reader2 = cmd2.ExecuteReader())
                            {
                                while (reader2.Read())
                                {
                                    current_list.Add(Convert.ToInt32(reader2["ID"]));
                                }
                            }
                        }
                    }

                    pastStringBuilder.Append("0 = 1;");

                    converter = new ForumConverter(userRepository.GetUserDictionaryForCreator(pastStringBuilder.ToString()));

                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + pastStringBuilder.ToString(), connection);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        forumPublications = new List<ForumPublication>();
                        while (reader.Read())
                        {
                            forumPublications.Add((ForumPublication)converter.ConvertFromDTO(reader));
                        }
                    }

                    return forumPublications.ToArray();
                }
            }
            catch
            {
                throw new DatabaseException();
            }

        }

        //public ForumPublicationWithReplies[] SearchForQuestionForumPublications(string query, int number)
        //{
        //    return GetForumPublicationsWithReplies("SELECT TOP @Number * FROM ForumPublications WHERE Text LIKE %@Query%;", new { Number = number, Query = query });
        //}

        //private ForumPublicationWithReplies[] GetPublicationsWithNumber(string sqlQuery, int number, object parameters = null)
        //{
        //    return GetForumPublicationsWithReplies($"SELECT TOP {number} * FROM " + sqlQuery, parameters);
        //}

        private ForumPublicationWithReplies[] GetForumPublicationsWithReplies(string sqlQuery, int? number, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmd;

                    if (number != null)
                        cmd = new SqlCommand($"SELECT TOP {number} * FROM " + sqlQuery, connection);
                    else
                        cmd = new SqlCommand("SELECT ForumPublications.* FROM " + sqlQuery, connection);

                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                        }
                    }

                    List<ForumPublication> publications = new List<ForumPublication>();


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ForumConverter converter = new ForumConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
                        while (reader.Read())
                        {
                            publications.Add((ForumPublication)converter.ConvertFromDTO(reader));
                        }
                    }

                    int length = publications.Count;
                    int[] replies = new int[length];
                    List<int>[] current_lists = new List<int>[length];
                    List<int>[] future_lists = new List<int>[length];
                    bool continue_loop = true;

                    for (int i = 0; i < length; i++)
                    {
                        current_lists[i] = new List<int> { publications[i].ID };
                        future_lists[i] = new List<int>();
                    }

                    while (continue_loop)
                    {
                        continue_loop = false;

                        for (int i = 0; i < length; i++)
                        {
                            if (current_lists[i].Count > 0)
                            {
                                future_lists[i].AddRange(connection.Query<int>("SELECT ID FROM ForumPublications WHERE " + String.Join(" OR ", current_lists[i].Select(item => $"QuestionPublicationID = {item}")) + ";"));
                                replies[i] += current_lists[i].Count;
                                current_lists[i].Clear();
                                current_lists[i].AddRange(future_lists[i]);
                                future_lists[i].Clear();
                                continue_loop = true;
                            }
                        }
                    }

                    //Dictionary<ForumPublication, int> dict_to_return = new Dictionary<ForumPublication, int>();
                    ForumPublicationWithReplies[] list_to_return = new ForumPublicationWithReplies[replies.Length];

                    for (int i = 0; i < length; i++)
                    {
                        list_to_return[i] = new ForumPublicationWithReplies(publications[i], replies[i] - 1);
                    }


                    //return dict_to_return;
                    return list_to_return;
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }

        public ForumPublicationWithReplies[] CastToPublicationsWithReplies(ForumPublication[] publications)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                int length = publications.Length;
                int[] replies = new int[length];
                List<int>[] current_lists = new List<int>[length];
                List<int>[] future_lists = new List<int>[length];
                bool continue_loop = true;

                for (int i = 0; i < length; i++)
                {
                    current_lists[i] = new List<int> { publications[i].ID };
                    future_lists[i] = new List<int>();
                }

                while (continue_loop)
                {
                    continue_loop = false;

                    for (int i = 0; i < length; i++)
                    {
                        if (current_lists[i].Count > 0)
                        {
                            future_lists[i].AddRange(connection.Query<int>("SELECT ID FROM ForumPublications WHERE " + String.Join(" OR ", current_lists[i].Select(item => $"QuestionPublicationID = {item}")) + ";"));
                            replies[i] += current_lists[i].Count;
                            current_lists[i].Clear();
                            current_lists[i].AddRange(future_lists[i]);
                            future_lists[i].Clear();
                            continue_loop = true;
                        }
                    }
                }

                //Dictionary<ForumPublication, int> dict_to_return = new Dictionary<ForumPublication, int>();
                ForumPublicationWithReplies[] list_to_return = new ForumPublicationWithReplies[publications.Length];

                for (int i = 0; i < length; i++)
                {
                    list_to_return[i] = new ForumPublicationWithReplies(publications[i], replies[i] - 1);
                }

                return list_to_return;
            }
            catch
            {
                throw new DatabaseException();
            }

            //return dict_to_return;
        }


        public Dictionary<ForumPublication, Dictionary<string, decimal>> GetNotWatchedPublicationsWithKeywords(User user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Dictionary<ForumPublication, Dictionary<string, decimal>> dict_to_return = new Dictionary<ForumPublication, Dictionary<string, decimal>>();

                    string sql = $@"ForumPublications 
                               WHERE QuestionPublicationID IS NULL
                               AND NOT ForumPublications.CreatorID = {user.Id}
                               AND NOT EXISTS (
                                  SELECT WatchedPublications.WatchedPublicationID 
                                  FROM WatchedPublications
                                  WHERE WatchedPublications.UserID = {user.Id}
                                  AND WatchedPublications.WatchedPublicationID = ForumPublications.ID)";

                    Dictionary<int, User> users = userRepository.GetUserDictionaryForCreator(sql, null);

                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + sql, connection);

                    List<ForumPublication> forumPublications = new List<ForumPublication>();

                    Dictionary<string, decimal> keyWords;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        using (SqlConnection connection2 = new SqlConnection(connectionString))
                        {
                            connection2.Open();
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["ID"]);
                                SqlCommand cmd2 = new SqlCommand("SELECT Keyword, Ranking FROM ForumKeywords WHERE ForumPublicationID = @ID", connection2);
                                cmd2.Parameters.AddWithValue("@ID", id);

                                keyWords = new Dictionary<string, decimal>();

                                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        keyWords.Add(
                                            reader2["Keyword"].ToString(),
                                            Convert.ToDecimal(reader2["Ranking"])
                                            );
                                    }
                                }

                                dict_to_return.Add(new ForumPublication(
                                    id,
                                    reader["Text"].ToString(),
                                    Convert.ToDateTime(reader["CreationDate"]),
                                    users[Convert.ToInt32(reader["CreatorID"])],
                                    Convert.ToInt32(reader["IsDeleted"]) == 1,
                                    null
                                    ), keyWords);

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

        public ForumPublication[] GetNotWatchedPublications(User current_user)
        {
            string sql = $@"ForumPublications
                           WHERE ForumPublications.QuestionPublicationID IS NULL
                           AND NOT ForumPublications.CreatorID = {current_user.Id}
                           AND NOT EXISTS 
                                 (SELECT WatchedPublications.WatchedPublicationID 
                                  FROM WatchedPublications
                                  WHERE WatchedPublications.UserID = {current_user.Id}
                                  AND WatchedPublications.WatchedPublicationID = ForumPublications.ID
                                 );";
            return InitConverterAndQueryDatabase(sql, null);
            //return GetForumPublicationsWithReplies(sql, null, new { UserID = current_user.Id });
        }

        //public ForumPublicationWithReplies[] GetRecommendationsByUser(User user)
        //{
        //    string sql = @"SELECT ForumPublications.*, UserCount 
        //                   FROM ForumPublications
        //                   INNER JOIN WatchedPublications
        //                   ON "
        //}
    }
}
