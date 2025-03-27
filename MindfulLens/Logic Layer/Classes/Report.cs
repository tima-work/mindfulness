using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Classes
{
    public class Report : Publication
    {
        public string Reason { get; private set; }
        public Publication ReportedPublication { get; private set; }
        public Report(string text, string reason, User creator, Publication reportedPublication) : this(0, text, DateTime.Now, creator, reason, reportedPublication)
        {

        }

        public Report(int id, string text, DateTime creationDate, User? creator, string reason, Publication reportedPublication) : base(id, text, creationDate, creator)
        {
            this.Reason = reason;
            this.ReportedPublication = reportedPublication;
        }

        public Report(string text, string reason, Publication reportedPublication) : this(0, text, DateTime.Now, null, reason, reportedPublication) { }
    }
}
