using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class ReviewManager
    {
        private IReviewRepository reviewRepository;

        public ReviewManager(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        //public Review[] GetAllReviews(int number)
        //{
        //    return reviewRepository.GetAllReviews(number);
        //}

        public Review[] GetReviewByCreator(User creator)
        {
            return reviewRepository.GetReviewByCreator(creator);
        }

        public void AddReview(Review review)
        {
            CheckReviewInfo(review);
            reviewRepository.AddReview(review);
        }

        public void DeleteReview(Review review)
        {
            reviewRepository.DeleteReview(review);
        }

        public void UpdateReview(Review review)
        {
            CheckReviewInfo(review);
            reviewRepository.UpdateReview(review);
        }

        //public Review[] SearchForReviews(string query, int number)
        //{
        //    return reviewRepository.SearchForReviews(query, number);
        //}

        //public Review[] SearchForReviews(Review[] reviews, string query)
        //{
        //    return reviews.Where(r => r.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToArray();
        //}

        private void CheckReviewInfo(Review review)
        {
            if (review.Text.Length < 5)
                throw new Exception("The review should be at least 5 characters long");
            if (review.Ranking < 1 || review.Ranking > 10)
                throw new Exception("The ranking should be between 1 and 10");
        }

        //public Review[] SortReviews(IComparer<Publication> comparer)
        //{
        //    Review[] reviews = reviewRepository.GetAllReviews();
        //    Array.Sort(reviews, comparer);
        //    return reviews;
        //}

        //public Review[] SortSomeReviews(Review[] reviews, IComparer<Publication> comparer)
        //{
        //    Array.Sort(reviews, comparer);
        //    return reviews;
        //}

        public Review? GetReviewById(int id)
        {
            return reviewRepository.GetReviewById(id);
        }

        public Review[] GetReviews(string? search_query, string? sortMethod, string? sortOrder, int number)
        {
            return reviewRepository.GetReviews(search_query, sortMethod, sortOrder, number);
        }
    }
}
