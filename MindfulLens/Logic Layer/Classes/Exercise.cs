using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class Exercise : TitledPublication
    {
        public bool IsOfficial { get; protected set; }


        public Exercise(string text, User creator, string title) : this(0, text, DateTime.Now, creator, title, false)
        {

        }

        public Exercise(int id, string text, DateTime creationDate, User creator, string title, bool isOfficial) : base(id, text, creationDate, creator, title)
        {
            this.IsOfficial = isOfficial;
        }
    }
}
