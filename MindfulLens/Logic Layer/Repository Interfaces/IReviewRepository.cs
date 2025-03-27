using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IReviewRepository
    {
        //Review[] GetAllReviews(int number);
        Review[] GetReviewByCreator(User creator);
        //Review[] SearchForReviews(string query, int number);
        Review[] GetReviews(string? search_query, string? sortMehtod, string? sortOrder, int number);
        Review? GetReviewById(int id);
        void AddReview(Review review);
        void DeleteReview(Review review);
        void UpdateReview(Review review);
    }
}
