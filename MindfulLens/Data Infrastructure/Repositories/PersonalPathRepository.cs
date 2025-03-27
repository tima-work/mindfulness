using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class PersonalPathRepository : ConnectionRepository, IPersonalPathRepository
    {
        private UserRepository userRepository;

        public PersonalPathRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public void AddPersonalPath(PersonalPath personalPath)
        {
            DoSimpleAction("INSERT INTO PersonalPaths (UserID, CurrentPlace) VALUES (@UserID, @CurrentPlace);", new { UserID = personalPath.Pupil.Id, CurrentPlace = personalPath.CurrentPlace });
        }

        public void DeletePersonalPath(PersonalPath personalPath)
        {
            DoSimpleAction("DELETE FROM PersonalPaths WHERE ID = @ID", personalPath);
        }

        public void UpdatePersonalPath(PersonalPath personalPath)
        {
            DoSimpleAction("UPDATE PersonalPaths SET CurrentPlace = @CurrentPlace WHERE ID = @ID;", new { personalPath.CurrentPlace, personalPath.Id });
        }

        public PersonalPath? GetPersonalPathByUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM PersonalPaths WHERE UserID = @UserID", connection);
                cmd.Parameters.AddWithValue("@UserID", user.Id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new PersonalPath(
                            Convert.ToInt32(reader["ID"]),
                            Convert.ToInt32(reader["CurrentActionID"]),
                            userRepository.GetUserByID(Convert.ToInt32(reader["UserID"]))
                            );
                    }
                }
            }
            return null;
        }
    }
}
