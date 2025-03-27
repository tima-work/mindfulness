using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public abstract class Publication
    {

        public int ID { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public string Text {  get; protected set; }
        public User? Creator { get; protected set; }


        public Publication(string text, User? creator) : this(0, text, DateTime.Now, creator)
        {

        }


        public Publication(int id, string text, DateTime creationDate, User? creator)
        {
            this.ID = id;
            this.Text = text;
            this.CreationDate = creationDate;
            this.Creator = creator;
        }
    }
}
