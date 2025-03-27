using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindfulLens_WebApp.Models;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class New_ReplyModel : PageModel
    {
        [BindProperty]
        public ForumPublicationModel forumPublicationModel { get; set; }

        public string errorMessage { get; private set; } = "";
        private ForumManager forumManager;
        private ForumRepository forumRepository;
        private UserRepository userRepository;
        private UserManager userManager;
        public ForumPublication? questionPublication { get; private set; } = null;

        public New_ReplyModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            forumRepository = new ForumRepository(userRepository);
            forumManager = new ForumManager(forumRepository);
        }

        public void OnGet(int? question_id)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            if (question_id == null)
                questionPublication = null;
            else 
                questionPublication = forumManager.GetForumPublicationById((int)question_id);
        }

        public IActionResult OnPostNewReply()
        {
            string? question_id = Request.Form["QuestionPublicationId"];
            questionPublication = forumManager.GetForumPublicationById(Convert.ToInt32(question_id));
            if (ModelState.IsValid)
            {
                try
                {
                    Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                        if (user != null && questionPublication != null)
                        {
                            forumManager.AddForumPublication(new ForumPublication(forumPublicationModel.Text, user, questionPublication));
                            return Redirect("/Successfully-Published");
                        }
                    }

                    errorMessage = "Something went wrong. Please, try again";
                }
                catch (WrongInputException ex)
                {
                    errorMessage = ex.Message;
                }
            }
            return Page();
        }
    }
}
