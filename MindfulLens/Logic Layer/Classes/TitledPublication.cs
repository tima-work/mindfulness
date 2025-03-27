using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Classes
{
    public abstract class TitledPublication : Publication
    {
        public string Title { get; protected set; }

        public TitledPublication(int id, string text, DateTime creationDate, User creator, string title) : base(id, text, creationDate, creator)
        {
             this.Title = title;
        }
    }
}
