using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using MindfulLens_WebApp.Models;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class Edit_Forum_PublicationModel : PageModel
    {
        [BindProperty]
        public ForumPublicationModel forumModel { get; set; }

        public string errorMessage { get; private set; } = "";
        public ForumPublication? forumPublication { get; private set; } = null;
        private UserRepository userRepository;
        private UserManager userManager;
        private ForumRepository repository;
        private ForumManager manager;
        public bool allowEdit { get; private set; }

        public Edit_Forum_PublicationModel()
        {
            userRepository = new UserRepository();
            repository = new ForumRepository(userRepository);
            manager = new ForumManager(repository);
        }

        public void OnGet(int? publication_id)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            Claim? claim_id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (publication_id != null)
                forumPublication = manager.GetForumPublicationById((int)publication_id);
            if (forumPublication != null)
            {
                if (claim_id != null && forumPublication.Creator.Id.ToString() == claim_id.Value)
                {
                    allowEdit = true;
                    forumModel = new ForumPublicationModel();
                    forumModel.Text = forumPublication.Text;
                }
                else
                    allowEdit = false;
            }
        }

        public IActionResult OnPostEditPublication()
        {
            if (ModelState.IsValid)
            {
                string? question_id = Request.Form["publication-id"];
                forumPublication = manager.GetForumPublicationById(Convert.ToInt32(question_id));

                if (forumPublication != null)
                {
                    try
                    {
                        ForumPublication newForumPublication = new ForumPublication(forumPublication.ID, forumModel.Text, forumPublication.CreationDate, forumPublication.Creator, forumPublication.IsDeleted, forumPublication.QuestionPublication);
                        manager.UpdateForumPublication(newForumPublication);
                        return Redirect("/Successfully-Edited");
                    }
                    catch (WrongInputException ex)
                    {
                        errorMessage = ex.Message;
                    }
                }
            }
            return Page();
        }
    }
}
