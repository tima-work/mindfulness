using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class ReportManager
    {
        private IReportRepository reportRepository;
        private IForumRepository forumRepository;
        private IReviewRepository reviewRepository;
        private ICognitivePartRepository cognitivePartRepository;
        private ISourceRepository sourceRepository;
        private IExerciseRepository exerciseRepository;

        public ReportManager(IReportRepository reportRepository, IForumRepository forumRepository, IReviewRepository reviewRepository, ICognitivePartRepository cognitivePartRepository, ISourceRepository sourceRepository, IExerciseRepository exerciseRepository)
        {
            this.reportRepository = reportRepository;
            this.forumRepository = forumRepository;
            this.reviewRepository = reviewRepository;
            this.cognitivePartRepository = cognitivePartRepository;
            this.sourceRepository = sourceRepository;
            this.exerciseRepository = exerciseRepository;
        }

        public void AddReport(Report report)
        {
            CheckReportInfo(report);
            reportRepository.AddReport(report);
        }

        public void AcceptReport(Report report)
        {
            //Delete reported publication
            reportRepository.DeleteReportsByPublication(report.ReportedPublication);
            Publication reportedPublication = report.ReportedPublication;
            switch (reportedPublication.GetType().Name)
            {
                case "ForumPublication":
                    forumRepository.DeleteForumPublication(reportedPublication as ForumPublication);
                    break;
                case "Review":
                    reviewRepository.DeleteReview(reportedPublication as Review);
                    break;
                case "CognitivePart":
                    cognitivePartRepository.DeleteCognitivePart(reportedPublication as CognitivePart);
                    break;
                case "Source":
                    sourceRepository.DeleteSource(reportedPublication as Source);
                    break;
                case "Exercise":
                    exerciseRepository.DeleteExercise(reportedPublication as Exercise);
                    break;

            }
        }

        public void DeclineReport(Report report)
        {
            reportRepository.DeleteReport(report);
        }

        //public Report[] GetReports(int number)
        //{
        //    return reportRepository.GetReports(number);
        //}

        //public Report[] GetAutoReports(int number)
        //{
        //    return reportRepository.GetAutoReports(number);
        //}

        public Report[] GetAllReportsByCreator(User creator)
        {
            return reportRepository.GetAllReportsByCreator(creator);
        }

        //public Report[] SearchForReports(string query, int number)
        //{
        //    return reportRepository.SearchForReports(query, number);
        //}

        //public Report[] SearchForReports(Report[] reports, string query)
        //{
        //    return reports.Where(r => r.Text.Contains(query, StringComparison.InvariantCultureIgnoreCase) || r.Reason.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToArray();
        //}

        //public Report[] SearchForAutoReports(string query, int number)
        //{
        //    return reportRepository.SearchForAutoReports(query, number);
        //}


        private void CheckReportInfo(Report report)
        {
            if (String.IsNullOrWhiteSpace(report.Reason))
                throw new Exception("You haven't entered reason of report");
            if (String.IsNullOrWhiteSpace(report.Text))
                throw new Exception("You haven't entered description of report");
        }

        //public Report[] SortReports(IComparer<Publication> comparer)
        //{
        //    return SortSomeReports(reportRepository.GetReports(), comparer);
        //}

        //public Report[] SortAutoReports(IComparer<Publication> comparer)
        //{
        //    return SortSomeReports(reportRepository.GetAutoReports(), comparer);
        //}

        //public Report[] SortSomeReports(Report[] reports, IComparer<Publication> comparer)
        //{
        //    Array.Sort(reports, comparer);
        //    return reports;
        //}

        public Report[] GetReports(string? search_query, string? sortMethod, string? sortOrder, bool isAuto, int number)
        {
            return reportRepository.GetReports(search_query, sortMethod, sortOrder, isAuto, number);
        }

        public Report? GetReportById(int id)
        {
            return reportRepository.GetReportById(id);
        }
    }
}
