using Dapper;
using Data_Infrastructure.Converters;
using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure
{
    public class ConnectionRepository
    {
        public static string connectionString = "Server=mssqlstud.fhict.local;Database=dbi544392_mindfulens;User Id=dbi544392_mindfulens;Password=Yyi45Ny6Xa;TrustServerCertificate=True;";

        protected Publication[] GetData(string sqlQuery, IDTOConverter converter, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    string a;

                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters).ToString());
                            //a = $"@{property.Name} {property.GetValue(parameters)}";
                        }
                    }

                    //cmd = cmd;

                    List<Publication> publications = new List<Publication>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            publications.Add(converter.ConvertFromDTO(reader));
                        }
                    }

                    return publications.ToArray();
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }


        protected Dictionary<int, Publication> GetDictionary(string sqlQuery, IDTOConverter converter, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM " + sqlQuery, connection);

                    if (parameters != null)
                    {
                        foreach (var property in parameters.GetType().GetProperties())
                        {
                            cmd.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                        }
                    }

                    Dictionary<int, Publication> publications = new Dictionary<int, Publication>();
                    Publication publication;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            publication = converter.ConvertFromDTO(reader);
                            publications.Add(publication.ID, publication);
                        }
                    }

                    return publications;
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }


        protected void DoSimpleAction(string sqlQuery, object parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute(sqlQuery, parameters);
                }
            }
            catch
            {
                throw new DatabaseException();
            }
        }
    }
}
