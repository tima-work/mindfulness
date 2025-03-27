using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindfulLens_WebApp.Models;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class New_ReviewModel : PageModel
    {
        [BindProperty]
        public ReviewModel reviewModel {  get; set; }

        public string errorMessage { get; set; } = "";
        private UserRepository userRepository;
        private UserManager userManager;
        private ReviewRepository reviewRepository;
        private ReviewManager reviewManager;

        public New_ReviewModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
            reviewRepository = new ReviewRepository(userRepository);
            reviewManager = new ReviewManager(reviewRepository);
        }

        public void OnGet()
        {
            WebFunctions.CheckIfBanned(HttpContext);
        }

        public IActionResult OnPostNewReview()
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
                            reviewManager.AddReview(new Review(reviewModel.Text, user, reviewModel.Ranking));
                            return Redirect("/Reviews-And-Feedback");
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
