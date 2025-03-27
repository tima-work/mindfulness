using Dapper;
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
    public class SourceRepository : ConnectionRepository, ISourceRepository
    {
        private UserRepository userRepository;

        public SourceRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddSource(Source source)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Sources (Text, CreationDate, CreatorID, Title, Author, IsOfficial) VALUES (@Text, @CreationDate, @CreatorID, @Title, @Author, @IsOfficial);", new { Text = source.Text, CreationDate = source.CreationDate, CreatorID = source.Creator.Id, Title = source.Title, Author = source.Author, IsOfficial = Convert.ToInt32(source.IsOfficial) });

                Source? current_source = GetSourceByAuthorAndTitle(source.Author, source.Title);

                if (current_source != null)
                {
                    foreach (string link in source.GetLinks())
                    {
                        connection.Execute("INSERT INTO SourcesLinks (SourceID, Link) VALUES (@SourceID, @Link);", new { SourceID = current_source.ID, Link = link });
                    }
                }
            }
        }

        public void DeleteSource(Source source)
        {
            DoSimpleAction("DELETE FROM Sources WHERE ID = @ID; DELETE FROM SourcesLinks WHERE SourceID = @ID;", new {ID = source.ID});
        }

        //public Source[] GetAllSources(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Sources";
        //    object parameters = new { Number = number };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Source[] GetCustomSources(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Sources WHERE IsOfficial = 0;";
        //    object parameters = new {Number = number};
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Source[] GetOfficialSources(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Sources WHERE IsOfficial = 1;";
        //    object parameters = new { Number = number };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Source[] GetOfficialSortedSources(int number, string sortMethod, string sortOrder)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Sources WHERE IsOfficial = 1 ORDER BY @SortMethod @SortOrder;";
        //    object parameters = new { Number = number, SortMethod = sortMethod, SortOrder = sortOrder };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public Source? GetSourceByAuthorAndTitle(string author, string title)
        {
            string sqlQuery = "Sources WHERE Title = @Title AND Author = @Author";
            object parameters = new { Title = title, Author = author };
            Source[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        public Source[] GetSourcesByUser(User creator)
        {
            string sqlQuery = "SELECT * FROM Sources WHERE CreatorID = @CreatorID";
            object parameters = new { CreatorID = creator.Id };
            return InitConverterAndQueryDatabase(sqlQuery, parameters);
        }

        public Source? GetSourceById(int id)
        {
            string sqlQuery = "Sources WHERE ID = @ID";
            object parameters = new { ID = id };
            Source[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        //public Source[] SearchForSources(string query, bool isOfficial, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Sources WHERE IsOfficial = @IsOfficial AND (Title LIKE %@Query% OR Text LIKE %@Query% OR Author LIKE %@Query%);";
        //    object parameters = new { Number = number, IsOfficial = isOfficial, Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Source[] SearchForSortedSources(string query, bool isOfficial, int number, string sortMethod, string sortOrder)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Sources WHERE IsOfficial = @IsOfficial AND (Title LIKE %@Query% OR Text LIKE %@Query% OR Author LIKE %@Query%) ORDER BY @SortMethod @SortOrder;";
        //    object parameters = new { Number = number, IsOfficial = isOfficial, Query = query, SortMethod = sortMethod, SortOrder = sortOrder };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public void UpdateSource(Source source)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            {
                connection.Open();
                connection.Execute("UPDATE Sources SET Text = @Text, Title = @Title, Author = @Author WHERE ID = @ID;", source);

                connection.Execute("DELETE FROM SourcesLinks WHERE SourceID = @ID", source.ID);

                foreach (string link in source.GetLinks())
                {
                    connection.Execute("INSERT INTO SourcesLinks (SourceID, Link) VALUES (@SourceID, @Link);", new { SourceID = source.ID, Link = link });
                }
            }
        }

        public void MakeSourceOfficial(Source source)
        {
            ChangeSourceOfficiality(source, true);
        }

        public void MakeSourceUnofficial(Source source)
        {
            ChangeSourceOfficiality(source, false);
        }

        private void ChangeSourceOfficiality(Source source, bool isOfficial)
        {
            int a = Convert.ToInt32(isOfficial);
            DoSimpleAction("UPDATE Sources SET IsOfficial = @IsOfficial WHERE ID = @ID;", new { IsOfficial = a, ID = source.ID });
        }

        public Dictionary<int, List<string>> GetAllLinks(string sqlQuery, object parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT ID FROM " +  sqlQuery, connection);
                if (parameters != null)
                {
                    foreach (var property in parameters.GetType().GetProperties())
                    {
                        command.Parameters.AddWithValue("@" + property.Name, property.GetValue(parameters));
                    }
                }

                StringBuilder stringBuilder = new StringBuilder("SELECT * FROM SourcesLinks WHERE ");

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stringBuilder.Append($"SourceID = {Convert.ToInt32(reader["ID"])} OR ");
                    }
                }
                stringBuilder.Append("0 = 1");

                SqlCommand cmd = new SqlCommand(stringBuilder.ToString(), connection);

                Dictionary<int, List<string>> links = new Dictionary<int, List<string>>();
               
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int sourceId;
                    string link;
                    while (reader.Read())
                    {
                        sourceId = Convert.ToInt32(reader["SourceID"]);
                        link = reader["Link"].ToString();

                        if (links.ContainsKey(sourceId))
                            links[sourceId].Add(link);
                        else
                            links.Add(sourceId, new List<string>() { link });
                    }
                }

                return links;
            }
        }

        public Source[] CastToSource(Publication[] publications)
        {
            Source[] sources = new Source[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                sources[i] = (Source)publications[i];
            return sources;
        }

        public Dictionary<int, Publication> GetSourceDictionary(string sqlQuery, object parameters = null)
        {
            //string sqlQuery = "SELECT * FROM Sources;";
            //object parameters = null;
            SourceConverter converter = new SourceConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters), GetAllLinks(sqlQuery, parameters));
            return GetDictionary(sqlQuery, converter, parameters);
        }

        private Source[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            SourceConverter converter = new SourceConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters), GetAllLinks(sqlQuery, parameters));
            return CastToSource(GetData("SELECT * FROM " + sqlQuery, converter, parameters));
        }

        private Source[] GetSourcesWithNumber(string sqlQuery, object parameters, int number)
        {
            SourceConverter converter = new SourceConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters), GetAllLinks(sqlQuery, parameters));
            return CastToSource(GetData($"SELECT TOP {number} * FROM " + sqlQuery, converter, parameters));
        }

        public Source[] GetSources(string? search_query, string? sortMethod, string? sortOrder, bool? isOfficial, int number)
        {
            string sqlQuery;
            object parameters;
            if (search_query != null)
            {
                if (isOfficial != null)
                {
                    sqlQuery = "Sources WHERE IsOfficial = @IsOfficial AND (Text LIKE '%' + @Query + '%' OR Title LIKE '%' + @Query + '%' OR Author LIKE '%' + @Query + '%')";
                    parameters = new { IsOfficial = Convert.ToInt32(isOfficial), Query = search_query };
                }
                else
                {
                    sqlQuery = "Sources WHERE Text LIKE '%' + @Query + '%' OR Title LIKE '%' + @Query + '%' OR Author LIKE '%' + @Query + '%'";
                    parameters = new { Query = search_query };
                }
            }
            else
            {
                if (isOfficial != null)
                {
                    sqlQuery = "Sources WHERE IsOfficial = @IsOfficial";
                    parameters = new { IsOfficial = isOfficial };
                }
                else
                {
                    sqlQuery = "Sources";
                    parameters = new { };
                }
            }
            if (sortMethod != null && sortOrder != null)
            {
                sqlQuery = $"{sqlQuery} ORDER BY {sortMethod} {sortOrder}";
            }

            return GetSourcesWithNumber(sqlQuery, parameters, number);
        }
    }
}
