using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class CognitivePart : Exercise
    {
        public string WayOfHandling { get; private set; }
        public string ClassName { get; private set; }
        public CognitivePart(string text, User creator, string title, string className, string wayOfHandling) : this(0, text, DateTime.Now, creator, title, className, wayOfHandling, false)
        {

        }

        public CognitivePart(int id, string text, DateTime creationDate, User creator, string title, string className, string wayOfHandling, bool isOfficial) : base(id, text, creationDate, creator, title, isOfficial)
        {
            this.WayOfHandling = wayOfHandling;
            this.ClassName = className;
        }
    }
}
