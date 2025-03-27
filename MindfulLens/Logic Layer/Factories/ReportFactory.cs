using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Factories
{
    public class ReportFactory
    {
        private string[] report_actions = new string[2] { "Accept", "Decline" };
        public ReportFactory() { }

        public string? GetReportAction(string action)
        {
            return report_actions.Contains(action) ? action : null;
        }
    }
}
