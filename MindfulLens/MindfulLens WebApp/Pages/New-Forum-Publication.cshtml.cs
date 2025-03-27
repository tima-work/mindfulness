using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Managers;
using Logic_Layer.Publication_cleaners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindfulLens_WebApp.Models;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "User")]
    public class New_Forum_PublicationModel : PageModel
    {
        [BindProperty]
        public ForumPublicationModel forumPublicationModel { get; set; }

        public string errorMessage { get; private set; } = "";

        private UserRepository userRepository;
        private ForumRepository forumRepository;

        private UserManager userManager;
        private ForumManager forumManager;


        public New_Forum_PublicationModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);


            forumRepository = new ForumRepository(userRepository);
            forumManager = new ForumManager(forumRepository);
        }

        public void OnGet()
        {
            WebFunctions.CheckIfBanned(HttpContext);
        }

        public IActionResult OnPostNewForumPublication()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                        if (user != null)
                        {
                            forumManager.AddForumPublication(new ForumPublication(forumPublicationModel.Text, user));
                            return Redirect("/Forum");
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
