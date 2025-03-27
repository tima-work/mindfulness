using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Logic_Layer.Classes
{
    public class Content : TitledPublication
    {
        public string ClassName { get; private set; }
        public byte[] Image { get; private set; }
        public Content(string text, User creator, string title, string className, byte[] image) : this(0, text, DateTime.Now, creator, title, className, image)
        {

        }


        public Content(int id, string text, DateTime creationDate, User creator, string title, string className, byte[] image) : base(id, text, creationDate, creator, title)
        {
            this.ClassName = className;
            Image = image;
        }
    }
}
