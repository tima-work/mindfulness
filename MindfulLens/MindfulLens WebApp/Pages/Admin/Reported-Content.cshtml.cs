using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Me")]
    public class Reported_ContentModel : PageModel
    {
        public string? search_query { get; private set; } = null;
        public string? sort_method { get; private set; } = null;
        public string? sort_order { get; private set; } = null;
        private ReportManager reportManager;
        private ReportRepository reportRepository;
        private ReviewRepository reviewRepository;
        private ForumRepository forumRepository;
        private SourceRepository sourceRepository;
        private CognitivePartRepository cognitivePartRepository;
        private ExerciseRepository exerciseRepository;
        private UserRepository userRepository;
        private SortFactory sortFactory;
        private ReportFactory reportFactory;
        public Report[] reports;
        public int load_number { get; private set; }
        public const int load_step = 3;
        public bool showMoreButton { get; private set; }

        public Reported_ContentModel()
        {
            userRepository = new UserRepository();

            reviewRepository = new ReviewRepository(userRepository);
            forumRepository = new ForumRepository(userRepository);
            sourceRepository = new SourceRepository(userRepository);
            cognitivePartRepository = new CognitivePartRepository(userRepository);
            exerciseRepository = new ExerciseRepository(userRepository);

            reportRepository = new ReportRepository(userRepository, reviewRepository, forumRepository, sourceRepository, cognitivePartRepository, exerciseRepository);

            reportManager = new ReportManager(reportRepository, forumRepository, reviewRepository, cognitivePartRepository, sourceRepository, exerciseRepository);

            sortFactory = new OnlyTimeSortFactory();

            reportFactory = new ReportFactory();
        }

        public void OnGet(string? search, string? sort, string? order, int? number, int? report_id, string? report_action)
        {
            WebFunctions.CheckIfBanned(HttpContext);

            if (report_action != null && report_id != null)
            {
                string? found_report_action = reportFactory.GetReportAction(report_action);
                if (found_report_action != null)
                {
                    Report? report = reportManager.GetReportById(Convert.ToInt32(report_id));
                    if (report != null)
                    {
                        switch (found_report_action)
                        {
                            case "Accept":
                                reportManager.AcceptReport(report);
                                break;
                            case "Decline":
                                reportManager.DeclineReport(report);
                                break;
                        }

                        WebFunctions.ShortenPageURL(search, sort, order, number, "Admin/Reported-Content", HttpContext);
                        //string search_var = "";
                        //string sort_var = "";
                        //string order_var = "";
                        //string number_var = "";
                        //if (search != null)
                        //    search_var = $"search={search}&";
                        //if (sort != null)
                        //    sort_var = $"sort={sort}&";
                        //if (order != null)
                        //    order_var = $"order={order}&";
                        //if (number != null)
                        //    number_var = $"number={number}";
                        //HttpContext.Response.Redirect($"/Reported-Content?{search_var}{sort_var}{order_var}{number_var}");
                    }
                }
            }

            search_query = search;
            sort_method = sort;
            sort_order = order;

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            Tuple<string?, string?> sort_setup = CommonData.CheckSort(sort, order, sortFactory);
            sort_method = sort_setup.Item1;
            sort_order = sort_setup.Item2;

            reports = reportManager.GetReports(search_query, sort_method, sort_order, false, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref reports, load_number);
        }

        public void OnPostAcceptReport()
        {
            string? report_id = Request.Form["report-id"];

            if (report_id != null)
            {
                Report? report = reportManager.GetReportById(Convert.ToInt32(report_id));
                if (report != null)
                {
                    reportManager.AcceptReport(report);
                }
            }
            //return Page();
        }

        public IActionResult OnPostDeclineReport()
        {
            string? report_id = Request.Form["report-id"];

            if (report_id != null)
            {
                Report? report = reportManager.GetReportById(Convert.ToInt32(report_id));
                if (report != null)
                {
                    reportManager.DeclineReport(report);
                }
            }
            return Page();
        }
    }
}
