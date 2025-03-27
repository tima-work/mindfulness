using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using Data_Infrastructure.Converters;
using Logic_Layer.Classes;

namespace Data_Infrastructure.Repositories
{
    public class CognitivePartRepository : ConnectionRepository, ICognitivePartRepository
    {
        private UserRepository userRepository;

        public CognitivePartRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddCognitivePart(CognitivePart cognitivePart)
        {
            DoSimpleAction("INSERT INTO CognitiveParts (Text, CreationDate, CreatorID, Title, WayOfHandling, ClassName, IsOfficial) VALUES (@Text, @CreationDate, @CreatorID, @Title, @WayOfHandling, @ClassName, @IsOfficial);", new {Text = cognitivePart.Text, CreationDate = cognitivePart.CreationDate, CreatorID = cognitivePart.Creator.Id, Title = cognitivePart.Title, WayOfHandling = cognitivePart.WayOfHandling, ClassName = cognitivePart.ClassName, IsOfficial = cognitivePart.IsOfficial});
            //using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            //{
            //    connection.Execute("INSERT INTO CognitiveParts (ID, Text, CreationDate, CreatorID, Title, WayOfHandling, ClassName, IsOfficial) VALUES (@ID, @Text, @CreationDate, @CreatorID, @Title, @WayOfHandling, @ClassName, @IsOfficial);", cognitivePart);
            //}
        }

        public void DeleteCognitivePart(CognitivePart cognitivePart)
        {
            DoSimpleAction("DELETE FROM CognitiveParts WHERE ID = @ID;", cognitivePart);
            //using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            //{
            //    connection.Execute("DELETE FROM CognitiveParts WHERE ID = @ID;", cognitivePart);
            //}
        }

        //public CognitivePart[] GetCognitivePartsByClassname(string classname, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM CognitiveParts WHERE ClassName = @ClassName;";
        //    object parameters = new { Number = number, ClassName = classname };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public CognitivePart? GetCognitivePartByTitleAndClassname(string title, string classname)
        {
            string sqlQuery = "CognitiveParts WHERE ClassName = @ClassName AND Title = @Title";
            object parameters = new { ClassName = classname, Title = title };
            CognitivePart[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        public CognitivePart[] GetCognitivePartsByCreator(User creator, string classname)
        {
            string sqlQuery = "SELECT * FROM CognitiveParts WHERE ClassName = @ClassName AND CreatorID = @CreatorID;";
            object parameters = new { ClassName = classname, CreatorID = creator.Id };
            return InitConverterAndQueryDatabase(sqlQuery, parameters);
        }

        //public CognitivePart[] GetCustomCognitiveParts(string classname, int number)
        //{
        //    object parameters = new { Number = number, ClassName = classname };
        //    string sqlQuery = "SELECT TOP @Number * FROM CognitiveParts WHERE ClassName = @ClassName AND IsOfficial = 1;";
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public CognitivePart[] GetOfficialCognitiveParts(string classname, int number)
        //{
        //    object parameters = new { Number = number, ClassName = classname };
        //    string sqlQuery = "SELECT TOP @Number * FROM CognitiveParts WHERE ClassName = @ClassName AND IsOfficial = 0;";
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public CognitivePart[] GetOfficialSortedCognitiveParts(string classname, int number, string sortMethod, string sortOrder)
        //{
        //    object parameters = new { Number = number, ClassName = classname, SortMethod = sortMethod, SortOrder = sortOrder };
        //    string sqlQuery = "SELECT TOP @Number * FROM CognitiveParts WHERE ClassName = @ClassName AND IsOfficial = 0 ORDER BY @SortMethod @SortOrder;";
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public CognitivePart[] SearchForCognitiveParts(string query, string classname, bool isOfficial, int number)
        //{
        //    object parameters = new { Number = number, Query = query, IsOfficial = Convert.ToInt32(isOfficial), ClassName = classname };
        //    string sqlQuery = "SELECT TOP @Number * FROM CognitiveParts WHERE ClassName = @ClassName AND IsOfficial = @IsOfficial AND (Text Like %@Query% OR Title Like %@Query% or WayOfHandling LIKE %@Query%);";
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public CognitivePart[] SearchForSortedCognitiveParts(string query, string classname, bool isOfficial, int number, string sortMethod, string sortOrder)
        //{
        //    object parameters = new { Number = number, Query = query, IsOfficial = Convert.ToInt32(isOfficial), ClassName = classname, SortMethod = sortMethod, SortOrder = sortOrder };
        //    string sqlQuery = "SELECT TOP @Number * FROM CognitiveParts WHERE ClassName = @ClassName AND IsOfficial = @IsOfficial AND (Text Like %@Query% OR Title Like %@Query% or WayOfHandling LIKE %@Query%) ORDER BY @SortMethod @SortOrder;";
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public void UpdateCognitivePart(CognitivePart cognitivePart)
        {
            DoSimpleAction("UPDATE CognitiveParts SET Text = @Text, Title = @Title, WayOfHandling = @WayOfHandling WHERE ID = @ID;", cognitivePart);
            //using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            //{
            //    connection.Execute("UPDATE CognitiveParts SET Text = @Text, Title = @Title, WayOfHandling = @WayOfHandling WHERE ID = @ID;", cognitivePart);
            //}
        }

        public void MakeCognitivePartOfficial(CognitivePart cognitivePart)
        {
            ChangeCognitivePartOfficiality(cognitivePart, true);
        }

        public void MakeCognitivePartUnofficial(CognitivePart cognitivePart)
        {
            ChangeCognitivePartOfficiality(cognitivePart, false);
        }

        private void ChangeCognitivePartOfficiality(CognitivePart cognitivePart, bool isOfficial)
        {
            int a = Convert.ToInt32(isOfficial);
            DoSimpleAction("UPDATE CognitiveParts SET IsOfficial = @IsOfficial WHERE ID = @ID;", new { IsOfficial = a, ID = cognitivePart.ID });
            //using (SqlConnection connection = new SqlConnection(ConnectionRepository.connectionString))
            //{
            //    connection.Execute("UPDATE CognitiveParts SET IsOfficial = @IsOfficial WHERE ID = @ID;", new {isOfficial, cognitivePart.ID});
            //}
        }

        private CognitivePart[] CastToCognitivePart(Publication[] publications)
        {
            CognitivePart[] cognitiveParts = new CognitivePart[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                cognitiveParts[i] = (CognitivePart)publications[i];
            return cognitiveParts;
        }

        public CognitivePart? GetCognitivePartById(int id)
        {
            object parameters = new { ID = id };
            string sqlQuery = "CognitiveParts WHERE ID = @ID;";
            CognitivePart[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        public Dictionary<int, Publication> GetCognitivePartDictionary(string sqlQuery, object parameters = null)
        {
            //object parameters = null;
            //string sqlQuery = "SELECT * FROM CognitiveParts;";
            CognitivePartConverter converter = new CognitivePartConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return GetDictionary(sqlQuery, converter, parameters);
        }

        private CognitivePart[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            CognitivePartConverter converter = new CognitivePartConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToCognitivePart(GetData("SELECT * FROM " + sqlQuery, converter, parameters));
        }

        private CognitivePart[] GetCognitivePartsWithNumber(string sqlQuery, object parameters, int number)
        {
            CognitivePartConverter converter = new CognitivePartConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToCognitivePart(GetData($"SELECT TOP {number} * FROM " + sqlQuery, converter, parameters));
        }

        public CognitivePart[] GetCognitiveParts(string? search_query, string? sortMethod, string? sortOrder, bool? isOfficial, string className, int number)
        {
            string sqlQuery;
            object parameters;
            if (search_query != null)
            {
                if (isOfficial != null)
                {
                    sqlQuery = "CognitiveParts WHERE IsOfficial = @IsOfficial AND ClassName = @ClassName AND (Title LIKE '%' + @Query + '%' OR Text LIKE '%' + @Query + '%' OR WayOfHandling LIKE '%' + @Query + '%')";
                    parameters = new {isOfficial = Convert.ToInt32(isOfficial), ClassName = className, Query = search_query};
                }
                else
                {
                    sqlQuery = "CognitiveParts WHERE ClassName = @ClassName AND (Title LIKE '%' + @Query + '%' OR Text LIKE '%' + @Query + '%' OR WayOfHandling LIKE '%' + @Query + '%')";
                    parameters = new { ClassName = className, Query = search_query };
                }
            }
            else
            {
                if (isOfficial != null)
                {
                    sqlQuery = "CognitiveParts WHERE IsOfficial = @IsOfficial AND ClassName = @ClassName";
                    parameters = new { isOfficial = Convert.ToInt32(isOfficial), ClassName = className };
                }
                else
                {
                    sqlQuery = "CognitiveParts WHERE ClassName = @ClassName";
                    parameters = new { ClassName = className };
                }
            }
            if (sortMethod != null && sortOrder != null)
            {
                sqlQuery = $"{sqlQuery} ORDER BY {sortMethod} {sortOrder}";
            }

            return GetCognitivePartsWithNumber(sqlQuery, parameters, number);
        }
    }
}
