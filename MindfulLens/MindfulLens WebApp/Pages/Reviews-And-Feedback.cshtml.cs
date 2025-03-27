using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class ReviewsAndFeedbackModel : PageModel
    {
        public string? search_query { get; private set; } = null;
        public string? sort_method { get; private set; } = null;
        public string? sort_order { get; private set; } = null;
        private ReviewRepository repository;
        private UserRepository userRepository;
        private UserManager userManager;
        private ReviewManager manager;
        private SortFactory sortFactory;
        public Review[] reviews;
        public int load_number { get; private set; }
        public const int load_step = 3;
        public bool showMoreButton { get; private set; }

        public ReviewsAndFeedbackModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            repository = new ReviewRepository(userRepository);
            manager = new ReviewManager(repository);
            sortFactory = new RankingAndTimeSortFactory();
        }

        public void OnGet(string? search, string? sort, string? order, int? number, int? review_id)
        {
            if (review_id != null)
            {
                Review? review = manager.GetReviewById((int)review_id);
                if (review != null)
                {
                    Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                        if (user != null && review.Creator.Id == user.Id)
                        {
                            manager.DeleteReview(review);
                            WebFunctions.ShortenPageURL(search, sort, order, number, "Reviews-And-Feedback", HttpContext);
                        }
                    }
                }
            }

            search_query = search;
            sort_method = sort;
            sort_order = order;

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            Tuple<string?, string?> sort_setup = CommonData.CheckSort(sort, order, sortFactory);
            sort_method = sort_setup.Item1;
            sort_order = sort_setup.Item2;

            reviews = manager.GetReviews(search_query, sort_method, sort_order, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref reviews, load_number);
        }
    }
}
