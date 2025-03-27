using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class TrialReviewRepository : IReviewRepository
    {
        private List<Review> reviews;

        public TrialReviewRepository()
        {
            User user = new User("Bob", "bob@gmail.com", "bob12345", "bob");
            User user2 = new User("Tom", "tom@gmail.com", "tom12345", "tom");
            User creator = new User(1, user.Name, user.Email, user.HashedPassword, user.Salt, user.Username, user.Photo, user.Banned, user.Importancy);
            User creator2 = new User(2, user2.Name, user2.Email, user2.HashedPassword, user2.Salt, user2.Username, user2.Photo, user2.Banned, user2.Importancy);
            reviews = new List<Review>()
            {
                new Review(1, "Review 1", DateTime.Now, creator, 8),
                new Review(2, "Review 2", DateTime.Now, creator2, 3),
                new Review(3, "Review 3", DateTime.Now, creator, 6),
                new Review(4, "Review 4", DateTime.Now, creator, 9),
                new Review(5, "Review 5", DateTime.Now, creator, 2),
                new Review(6, "Review 6", DateTime.Now, creator, 5),
                new Review(7, "Review 7", DateTime.Now, creator, 7),
                new Review(8, "Review 8", DateTime.Now, creator, 1),
                new Review(9, "Review 9", DateTime.Now, creator, 10),
                new Review(10, "Review 10", DateTime.Now, creator, 4),
            };
        }
        public void AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Review[] GetAllReviews(int number)
        {
            return reviews.Take(number).ToArray();
        }

        public Review[] GetReviewByCreator(User creator)
        {
            throw new NotImplementedException();
        }

        public Review? GetReviewById(int id)
        {
            return reviews.FirstOrDefault(r => r.ID == id);
        }

        public Review[] GetReviews(string? search_query, string? sortMehtod, string? sortOrder, int number)
        {
            return reviews.Take(number).ToArray();
        }

        public Review[] SearchForReviews(string query, int number)
        {
            return reviews.Where(r => r.Text.Contains(query)).Take(number).ToArray();
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }
    }
}
