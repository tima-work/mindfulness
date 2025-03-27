using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MindfulLens_WebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Me")]
    public class Forum_BranchModel : PageModel
    {
        public bool? showBranch { get; private set; } = null;
        public bool showMoreButton { get; private set; }
        public int load_number { get; private set; }
        public const int load_step = 3;
        public int? publicationId { get; private set; }
        //private int? publicationId;
        private UserRepository userRepository;
        private UserManager userManager;
        private ForumRepository repository;
        private ForumManager manager;
        public ForumPublication[] forumPublications;

        public Forum_BranchModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            repository = new ForumRepository(userRepository);
            manager = new ForumManager(repository);
            load_number = load_step;
        }


        public void OnGet(int? publication_id, int? number, int? delete_publication_id)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            if (delete_publication_id != null)
            {
                ForumPublication? deleteForumPublication = manager.GetForumPublicationById((int)delete_publication_id);
                if (deleteForumPublication != null)
                {
                    manager.DeleteForumPublication(deleteForumPublication);
                    string publication_var = "";
                    string number_var = "";
                    if (publication_id != null)
                        publication_var = $"publication_id={publication_id}&";
                    if (number != null)
                        number_var = $"number={number}";
                    HttpContext.Response.Redirect($"/Admin/Forum-Branch?{publication_var}{number_var}");
                }
            }

            publicationId = publication_id;

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            if (publication_id == null)
            {
                showBranch = null;
                return;
            }
            ForumPublication? forumPublication = manager.GetForumPublicationById((int)publication_id);
            if (forumPublication == null || forumPublication.QuestionPublication != null)
            {
                showBranch = false;
                publicationId = null;
                return;
            }
            showBranch = true;
            forumPublications = manager.GetBranch(forumPublication, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
        }
    }
}
