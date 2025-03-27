using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Logic_Layer.Recommendations_creators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;
using Logic_Layer;

namespace MindfulLens_WebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Me")]
    public class ForumModel : PageModel
    {
        public string? search_query { get; private set; }
        private UserRepository userRepository;
        private UserManager userManager;
        private ForumRepository repository;
        private ForumManager manager;
        public ForumPublicationWithReplies[] forumPublications;
        public const int load_step = 3;
        public int load_number { get; private set; }
        //public string? load_way { get; private set; }
        public bool showMoreButton { get; private set; }

        public ForumModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            repository = new ForumRepository(userRepository);
            manager = new ForumManager(repository);
            load_number = load_step;
        }

        public void OnGet(string? search, int? number, int? publication_id)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            if (publication_id != null)
            {
                ForumPublication? forumPublication = manager.GetForumPublicationById((int)publication_id);
                if (forumPublication != null)
                {
                    manager.DeleteForumPublication(forumPublication);
                    WebFunctions.ShortenPageURL(search, null, null, number, "Admin/Forum", HttpContext);
                }
            }

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            search_query = search;

            forumPublications = manager.GetQuestionPublications(search_query, load_number + 1);


            showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
        }
    }
}
