using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using System.Collections;
using System.Dynamic;

namespace Data_Infrastructure.Repositories
{
    public class ContentRepository : ConnectionRepository, IContentRepository
    {
        private UserRepository userRepository;

        public ContentRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddContent(Content content)
        {
            DoSimpleAction("INSERT INTO Content (Text, CreationDate, CreatorID, Title, ClassName, Photo) VALUES (@Text, @CreationDate, @CreatorID, @Title, @ClassName, @Photo);", new { Text = content.Text, CreationDate = content.CreationDate, CreatorID = content.Creator.Id, Title = content.Title, ClassName = content.ClassName, Photo = content.Image });
        }

        public void DeleteContent(Content content)
        {
            DoSimpleAction("DELETE FROM Content WHERE ID = @ID;", content);
        }

        //public Content[] GetAllClassnames()
        //{
        //    using (SqlConnection connection = new SqlConnection(ConnectionString.connectionString))
        //    {
        //        return connection.Query<ClassnamePublication>("SELECT ")
        //    }
        //    throw new NotImplementedException();
        //}

        //public Content[] GetAllContent()
        //{
        //    throw new NotImplementedException();
        //}

        //public Content[] GetContentByClassname(string classname, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Content WHERE ClassName = @ClassName;";
        //    object parameters = new { Number = number, ClassName = classname };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Content[] GetContentByCreator(User creator)
        //{
        //    throw new NotImplementedException();
        //}

        public void UpdateContent(Content content)
        {
            DoSimpleAction("UPDATE Content SET Text = @Text, Title = @Title, Photo = @Image WHERE ID = @ID;", new { Text = content.Text, Title = content.Title, Image = content.Image, ID = content.ID});
        }

        //public Content[] SearchForContentWithClassname(string query, string classname, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Content WHERE ClassName = @ClassName AND (Title LIKE %@Query% OR Text LIKE %@Query%);";
        //    object parameters = new { Number = number, ClassName = classname, Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        //public Content[] SearchForAllContent(string query, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Content WHERE Title LIKE %@Query% OR Text LIKE %@Query%;";
        //    object parameters = new { Number = number, Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public Content? GetContentByTitleAndClassname(string title, string classname)
        {
            string sqlQuery = "Content WHERE ClassName = @ClassName AND Title = @Title";
            object parameters = new { ClassName = classname, Title = title };
            Content[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        //private ClassnamePublication[] GetSomeClassnames(SqlCommand cmd)
        //{
        //    using ()
        //}

        private Content[] CastToContent(Publication[] publications)
        {
            Content[] content = new Content[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                content[i] = (Content)publications[i];
            return content;
        }

        private Content[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            ContentConverter converter = new ContentConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToContent(GetData("SELECT * FROM " + sqlQuery, converter, parameters));
        }

        public Content? GetContentById(int id)
        {
            string sqlQuery = "Content WHERE ID = @ID;";
            object parameters = new { ID = id };
            Content[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        private Content[] GetContentWithNumber(string sqlQuery, object parameters, int number)
        {
            ContentConverter converter = new ContentConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToContent(GetData($"SELECT TOP {number} * FROM " + sqlQuery, converter, parameters));
        }

        public Content[] GetContent(string? search_query, string? sortMethod, string? sortOrder, string? filter_method, int number)
        {
            string sqlQuery;
            object parameters;
            if (search_query != null && filter_method != null)
            {
                sqlQuery = "Content WHERE ClassName = @ClassName AND (Text LIKE '%' + @Query + '%' OR Title LIKE '%' + @Query + '%')";
                parameters = new { ClassName = filter_method, Query = search_query };
            }
            else if (search_query != null)
            {
                sqlQuery = "Content WHERE Text LIKE '%' + @Query + '%' OR Title LIKE '%' + @Query + '%'";
                parameters = new { Query = search_query };
            }
            else if (filter_method != null)
            {
                sqlQuery = "Content WHERE ClassName = @ClassName";
                parameters = new { ClassName = filter_method };
            }
            else
            {
                sqlQuery = "Content";
                parameters = new { };
            }
            if (sortMethod != null && sortOrder != null)
            {
                sqlQuery = $"{sqlQuery} ORDER BY {sortMethod} {sortOrder}";
            }
            return GetContentWithNumber(sqlQuery, parameters, number);
            //return InitConverterAndQueryDatabase(sqlQuery, parameters);

        }

        //public Content[] GetAllContent(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Content";
        //    object parameters = new {Number = number};
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}
    }
}
