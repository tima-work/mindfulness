using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Converters
{
    public class ForumConverter : IDTOConverter
    {
        private Dictionary<int, User> users;

        public ForumConverter(Dictionary<int, User> users)
        {
            this.users = users;
        }

        public Publication ConvertFromDTO(SqlDataReader reader)
        {
            int? questionPublicationId = reader["QuestionPublicationID"] as int?;
            ForumPublication? questionPublication = null;
            if (questionPublicationId != null)
                questionPublication = GetQuestionPublication((int)questionPublicationId);
            return new ForumPublication(
                Convert.ToInt32(reader["id"]),
                reader["Text"].ToString(),
                Convert.ToDateTime(reader["CreationDate"]),
                users[Convert.ToInt32(reader["CreatorID"])],
                Convert.ToInt32(reader["IsDeleted"]) == 1,
                questionPublication
            );
        }

        public ForumPublication? GetQuestionPublication(int questionPublicationId)
        {
            using (SqlConnection connection2 = new SqlConnection(ConnectionRepository.connectionString))
            {
                connection2.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ForumPublications WHERE ID = @ID", connection2);
                cmd.Parameters.AddWithValue("@ID", questionPublicationId);

                using (SqlDataReader reader2 = cmd.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        return new ForumPublication(
                            Convert.ToInt32(reader2["ID"]),
                            reader2["Text"].ToString(),
                            Convert.ToDateTime(reader2["CreationDate"]),
                            users[Convert.ToInt32(reader2["CreatorID"])],
                            Convert.ToInt32(reader2["IsDeleted"]) == 1,
                            null
                        );
                    }
                }
            }
            return null;
        }
    }
}
