using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindfulLens_WebApp.Models;
using System.Linq;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class Edit_ReviewModel : PageModel
    {
        [BindProperty]
        public ReviewModel reviewModel {  get; set; }

        private UserRepository userRepository;
        private ReviewRepository repository;
        private ReviewManager manager;
        public bool allowEdit { get; private set; }
        public Review? review { get; private set; } = null;
        public string errorMessage {  get; private set; }

        public Edit_ReviewModel()
        {
            userRepository = new UserRepository();
            repository = new ReviewRepository(userRepository);
            manager = new ReviewManager(repository);
        }
        public void OnGet(int? review_id)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            Claim? claim_id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (review_id != null)
                review = manager.GetReviewById((int)review_id);
            if (review != null)
            {
                if (claim_id != null && review.Creator.Id.ToString() == claim_id.Value)
                {
                    allowEdit = true;
                    reviewModel = new ReviewModel(review);
                }
                else
                    allowEdit = false;
            }
        }

        public IActionResult OnPostEditReview()
        {
            string? review_id = Request.Form["review-id"];

            if (review_id != null)
            {
                review = manager.GetReviewById(Convert.ToInt32(review_id));
            }

            if (ModelState.IsValid && review != null)
            {
                try
                {
                    Review newReview = new Review(review.ID, reviewModel.Text, review.CreationDate, review.Creator, reviewModel.Ranking);
                    manager.UpdateReview(newReview);
                    return Redirect("/Reviews-And-Feedback");
                }
                catch (WrongInputException ex)
                {
                    errorMessage = ex.Message;
                    return Page();
                }
            }
            errorMessage = "Something went wrong. Please, try again";
            return Page();
        }
    }
}
