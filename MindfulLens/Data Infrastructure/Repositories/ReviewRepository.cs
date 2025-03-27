using Data_Infrastructure.Converters;
using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Infrastructure.Repositories
{
    public class ReviewRepository : ConnectionRepository, IReviewRepository
    {
        private UserRepository userRepository;

        public ReviewRepository(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void AddReview(Review review)
        {
            DoSimpleAction("INSERT INTO Reviews (CreationDate, Text, CreatorID, Ranking) VALUES (@CreationDate, @Text, @CreatorID, @Ranking);", new { CreationDate = review.CreationDate, Text = review.Text, CreatorID = review.Creator.Id, Ranking = review.Ranking });
        }

        public void DeleteReview(Review review)
        {
            DoSimpleAction("DELETE FROM Reviews WHERE ID = @ID;", review);
        }

        //public Review[] GetAllReviews(int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Reviews";
        //    object parameters = new { Number = number };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public Review[] GetReviewByCreator(User creator)
        {
            string sqlQuery = "SELECT * FROM Reviews WHERE CreatorID = @CreatorID;";
            object parameters = new { CreatorID = creator.Id };
            return InitConverterAndQueryDatabase(sqlQuery, parameters);
        }

        public Review? GetReviewById(int id)
        {
            string sqlQuery = "Reviews WHERE ID = @ID;";
            object parameters = new { ID = id };
            Review[] casted_array = InitConverterAndQueryDatabase(sqlQuery, parameters);
            if (casted_array.Length != 0)
                return casted_array[0];
            return null;
        }

        //public Review[] SearchForReviews(string query, int number)
        //{
        //    string sqlQuery = "SELECT TOP @Number * FROM Reviews WHERE Text LIKE %@Query%;";
        //    object parameters = new { Number = number, Query = query };
        //    return InitConverterAndQueryDatabase(sqlQuery, parameters);
        //}

        public void UpdateReview(Review review)
        {
            DoSimpleAction("UPDATE Reviews SET Text = @Text, Ranking = @Ranking WHERE ID = @ID;", review);
        }

        private Review[] CastToReview(Publication[] publications)
        {
            Review[] reviews = new Review[publications.Length];
            for (int i = 0; i < publications.Length; i++)
                reviews[i] = (Review)publications[i];
            return reviews;
        }

        //private Dictionary<int, Review> CastToReviewDictionary(Dict)

        public Dictionary<int, Publication> GetReviewDictionary(string sqlQuery, object parameters = null)
        {
            //string sqlQuery = "SELECT * FROM Reviews;";
            //object parameters = null;
            ReviewConverter converter = new ReviewConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return GetDictionary(sqlQuery, converter, parameters);
        }

        private Review[] InitConverterAndQueryDatabase(string sqlQuery, object parameters)
        {
            ReviewConverter converter = new ReviewConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToReview(GetData("SELECT * FROM " + sqlQuery, converter, parameters));
        }

        private Review[] GetReviewsWithNumber(string sqlQuery, object parameters, int number) 
        {
            ReviewConverter converter = new ReviewConverter(userRepository.GetUserDictionaryForCreator(sqlQuery, parameters));
            return CastToReview(GetData($"SELECT TOP {number} * FROM " + sqlQuery, converter, parameters));
        }

        public Review[] GetReviews(string? search_query, string? sortMethod, string? sortOrder, int number)
        {
            string sqlQuery;
            object parameters;

            if (search_query != null)
            {
                sqlQuery = "Reviews WHERE Text LIKE '%' + @Query + '%'";
                parameters = new { Query = search_query };
            }
            else
            {
                sqlQuery = "Reviews";
                parameters = new { };
            }
            if (sortMethod != null && sortOrder != null)
            {
                sqlQuery = $"{sqlQuery} ORDER BY {sortMethod} {sortOrder}";
            }

            return GetReviewsWithNumber(sqlQuery, parameters, number);
        }
    }
}
