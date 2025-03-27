using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class Forum_BranchModel : PageModel
    {
        public bool? showBranch { get; private set; } = null;
        public bool showMoreButton { get; private set; }
        public int load_number { get; private set; }
        public const int load_step = 3;
        public int? publicationId { get; private set; }
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
            if (delete_publication_id != null)
            {
                ForumPublication? deleteForumPublication = manager.GetForumPublicationById((int)delete_publication_id);
                if (deleteForumPublication != null)
                {
                    Claim? user_claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (user_claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(user_claim.Value));
                        if (user != null && deleteForumPublication.Creator.Id == user.Id)
                        {
                            manager.DeleteForumPublication(deleteForumPublication);
                            string publication_var = "";
                            string number_var = "";
                            if (publication_id != null)
                                publication_var = $"publication_id={publication_id}&";
                            if (number != null)
                                number_var = $"number={number}";
                            HttpContext.Response.Redirect($"/Forum-Branch?{publication_var}{number_var}");
                            //WebFunctions.ShortenPageURL(search, null, null, number, "Forum", HttpContext);
                        }
                    }
                }
            }

            publicationId = publication_id;

            if (number == null)
                load_number = load_step;
            else
                load_number = (int)number + load_step;

            if (publication_id == null)
            {
                showBranch = null;
                showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
                return;
            }
            ForumPublication? forumPublication = manager.GetForumPublicationById((int)publication_id);
            if (forumPublication == null || forumPublication.QuestionPublication != null)
            {
                showBranch = false;
                publicationId = null;
                showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
                return;
            }
            showBranch = true;
            forumPublications = manager.GetBranch(forumPublication, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref forumPublications, load_number);
        }
    }
}
