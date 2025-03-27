using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Me")]
    public class ReviewsModel : PageModel
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
        private const int load_step = 3;
        public bool showMoreButton { get; private set; }
        public ReviewsModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            repository = new ReviewRepository(userRepository);
            manager = new ReviewManager(repository);
            sortFactory = new RankingAndTimeSortFactory();
        }

        public void OnGet(string? search, string? sort, string? order, int? number, int? review_id)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            if (review_id != null)
            {
                Review? review = manager.GetReviewById((int)review_id);
                if (review != null)
                {
                    manager.DeleteReview(review);
                    WebFunctions.ShortenPageURL(search, sort, order, number, "Admin/Reviews-And-Feedback", HttpContext);
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
