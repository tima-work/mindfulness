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
    public class EmailProofRepository : ConnectionRepository, IEmailProofRepository
    {
        public void DeleteProofsByEmail(string email)
        {
            DoSimpleAction("DELETE FROM EmailProofs WHERE Email = @Email", new {Email = email});
        }

        public EmailProof? GetLastProofByEmail(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM EmailProofs WHERE Email = @Email ORDER BY CreationTime DESC;");
                command.Parameters.AddWithValue("@Email", email);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new EmailProof(
                        Convert.ToInt32(reader["ID"]),
                        reader["Email"].ToString(),
                        Convert.ToInt32(reader["Code"]),
                        Convert.ToDateTime(reader["CreationTime"])
                        );
                }
                return null;
            }
        }
    }
}
