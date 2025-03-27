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
using MindfulLens_WebApp.Pages.Admin;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class New_ReportModel : PageModel
    {
        [BindProperty]
        public ReportModel reportModel { get; set; }

        public string errorMessage { get; private set; } = "";

        private ForumRepository forumRepository;
        private ReviewRepository reviewRepository;
        private CognitivePartRepository cognitivePartRepository;
        private ExerciseRepository exerciseRepository;
        private SourceRepository sourceRepository;
        private ReportRepository reportRepository;
        private UserRepository userRepository;


        private ReviewManager reviewManager;
        private ForumManager forumManager;
        private ReportManager reportManager;
        private UserManager userManager;


        public Publication? ReportedPublication { get; private set; } = null;

        public string? publication_type { get; private set; } = null;

        public New_ReportModel()
        {
            userRepository = new UserRepository();
            forumRepository = new ForumRepository(userRepository);
            reviewRepository = new ReviewRepository(userRepository);
            cognitivePartRepository = new CognitivePartRepository(userRepository);
            exerciseRepository = new ExerciseRepository(userRepository);
            sourceRepository = new SourceRepository(userRepository);
            reportRepository = new ReportRepository(userRepository, reviewRepository, forumRepository, sourceRepository, cognitivePartRepository, exerciseRepository);


            reviewManager = new ReviewManager(reviewRepository);
            forumManager = new ForumManager(forumRepository);
            reportManager = new ReportManager(reportRepository, forumRepository, reviewRepository, cognitivePartRepository, sourceRepository, exerciseRepository);

            userManager = new UserManager(userRepository);
        }

        public void OnGet(int? publication_id, string? publication_type)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            this.publication_type = publication_type;
            if (publication_id != null && publication_type != null)
            {
                switch (publication_type)
                {
                    case "Review":
                        ReportedPublication = reviewManager.GetReviewById((int)publication_id);
                        break;
                    case "ForumPublication":
                        ReportedPublication = forumManager.GetForumPublicationById((int)publication_id);
                        break;
                }
            }
        }

        public IActionResult OnPostNewReport()
        {
            string? type = Request.Form["reported-publication-type"];
            string? id = Request.Form["reported-publication-id"];
            
            if (type != null && id != null)
            {
                switch (type)
                {
                    case "Review":
                        ReportedPublication = reviewManager.GetReviewById(Convert.ToInt32(id));
                        break;
                    case "ForumPublication":
                        ReportedPublication = forumManager.GetForumPublicationById(Convert.ToInt32(id));
                        break;
                }
            }

            if (ModelState.IsValid && ReportedPublication != null)
            {
                try
                {
                    Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                    if (claim != null)
                    {
                        User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                        if (user != null && ReportedPublication != null)
                        {
                            reportManager.AddReport(new Report(reportModel.Text, reportModel.Reason, user, ReportedPublication));
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
