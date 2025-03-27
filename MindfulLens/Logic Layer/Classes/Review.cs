using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Classes
{
    public class Review : Publication
    {
        public int Ranking { get; private set; }


        public Review(string text, User creator, int ranking) : this(0, text, DateTime.Now, creator, ranking)
        {

        }

        public Review(int id, string text, DateTime creationDate, User creator, int ranking) : base(id, text, creationDate, creator)
        {
            this.Ranking = ranking;
        }
    }
}
