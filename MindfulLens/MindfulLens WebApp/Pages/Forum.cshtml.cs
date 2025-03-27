using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Logic_Layer.Recommendations_creators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.AccessControl;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class ForumModel : PageModel
    {
        public string? search_query {  get; private set; }
        private UserRepository userRepository;
        private UserManager userManager;
        private ForumRepository repository;
        private ForumManager manager;
        public ForumPublicationWithReplies[] forumPublications;
        public const int load_step = 3;
        public int load_number { get; private set; }
        public bool showMoreButton { get; private set; }
        public string? recommendations_method { get; private set; }
        private AlgorithmFactory algorithmFactory;
        public ForumModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            repository = new ForumRepository(userRepository);
            manager = new ForumManager(repository);
            algorithmFactory = new AlgorithmFactory(repository, userRepository);
            load_number = load_step;
        }

        public void OnGet(string? search, string? recommendations, int? number, int? publication_id)
        {
            if (publication_id != null)
            {
                ForumPublication? forumPublication = manager.GetForumPublicationById((int)publication_id);
                if (forumPublication != null)
                {
                    Claim? user_claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (user_claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(user_claim.Value));
                        if (user != null && forumPublication.Creator.Id == user.Id)
                        {
                            manager.DeleteForumPublication(forumPublication);
                            WebFunctions.ShortenPageURL(search, null, null, number, "Forum", HttpContext);
                        }
                    }
                }
            }

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            search_query = search;

            recommendations_method = recommendations;

            IRecommendationsCreator? creator = algorithmFactory.GetRecommendationsCreator(recommendations);
            Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");

            if (creator != null && claim != null)
            {
                User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                if (user != null)
                {
                    WebFunctions.CheckIfBanned(HttpContext);
                    forumPublications = manager.CreateRecommendations(creator, user, load_number + 1);
                    showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
                    return;
                }
            }
            forumPublications = manager.GetQuestionPublications(search_query, load_number + 1);
            recommendations_method = null;

            showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
        }

        public IActionResult OnPostViewReplies()
        {
            string? branch_id = Request.Form["branch-id"];
            if (branch_id != null)
            {
                ForumPublication? forumPublication = manager.GetForumPublicationById(Convert.ToInt32(branch_id));
                if (forumPublication != null && forumPublication.QuestionPublication == null)
                {
                    Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                        if (user != null)
                        {
                            manager.WatchPublication(forumPublication, user);
                        }
                    }
                    return Redirect($"/Forum-Branch?publication_id={forumPublication.ID}");
                }
            }
            return Page();
        }
    }
}
