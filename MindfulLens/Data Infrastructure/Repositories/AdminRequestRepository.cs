using Logic_Layer.Classes;
using Logic_Layer.Repository_Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class AdminRequestRepository : ConnectionRepository, IAdminRequestRepository
    {
        private UserRepository userRepository;
        public AdminRequestRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public void AddAdminRequest(AdminRequest adminRequest)
        {
            int a = Convert.ToInt32(adminRequest.IsOffer);
            DoSimpleAction("INSERT INTO AdminRequests (UserID, Text, IsOffer, CreationDate) VALUES (@UserID, @Text, @IsOffer, @CreationDate);", new { UserID = adminRequest.Creator.Id, Text = adminRequest.Text, IsOffer = a, CreationDate = adminRequest.CreationDate });
        }

        public void DeleteAdminRequest(AdminRequest adminRequest)
        {
            DoSimpleAction("DELETE FROM AdminRequests WHERE ID = @ID;", new { ID = adminRequest.ID });
        }

        public void DeleteAdminRequestsByUser(User user)
        {
            DoSimpleAction("DELETE FROM AdminRequests WHERE UserID = @UserID;", new { UserId = user.Id });
        }

        public AdminRequest[] GetAdminRequestsByUser(User user)
        {
            return GetSomeAdminRequests("SEELCT * FROM AdminRequests WHERE UserID = @UserID;", new { UserID = user.Id });
        }

        public AdminRequest[] GetApplicationAdminRequests()
        {
            return GetSomeAdminRequests("SEELCT * FROM AdminRequests WHERE IsOffer = 0;");
        }

        private AdminRequest[] GetSomeAdminRequests(string sqlQuery, object parameters = null)
        {
            Dictionary<int, User> users = userRepository.GetUserDictionaryForUser(sqlQuery, parameters);
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

                List<AdminRequest> adminRequests = new List<AdminRequest>();
                AdminRequest adminRequest;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adminRequest = new AdminRequest(
                            Convert.ToInt32(reader["ID"]),
                            reader["Text"].ToString(),
                            Convert.ToDateTime(reader["CreationDate"]),
                            users[Convert.ToInt32(reader["UserID"])],
                            Convert.ToInt32(reader["IsOffer"]) == 1
                            );
                        adminRequests.Add(adminRequest);
                    }
                }

                return adminRequests.ToArray();
            }
        }
    }
}
