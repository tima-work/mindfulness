using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class PathStage
    {
        public int Id { get; private set; }
        public int Place {  get; private set; }
        public Exercise Action { get; private set; }

        public PathStage(int place, Exercise action) : this(0, place, action) { }

        public PathStage(int id, int place, Exercise action)
        {
            this.Id = id;
            this.Place = place;
            this.Action = action;
        }
    }
}
