using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IReportRepository
    {
        //Report[] GetReports(int number);
        //Report[] GetAutoReports(int number);
        Report[] GetAllReportsByCreator(User creator);
        //Report[] GetReportsByPublication(Publication publication);
        //Report[] SearchForReports(string query, int number);
        //Report[] SearchForAutoReports(string query, int number);
        Report[] GetReports(string? search_query, string? sortMethod, string? sortOrder, bool isAuto, int number);
        void AddReport(Report report);
        void DeleteReport(Report report);
        void DeleteReportsByPublication(Publication publication);
        Report? GetReportById(int id);
    }
}
