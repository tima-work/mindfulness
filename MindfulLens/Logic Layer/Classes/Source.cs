using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Classes
{
    public class Source : Exercise
    {
        public string Author { get; private set; }
        private List<string> links;
        public Source(string text, User creator, string title, string[] links, string author) : this(0, text, DateTime.Now, creator, title, links, author, false) { }

        public Source(int id, string text, DateTime creationDate, User creator, string title, string[] links, string author, bool isOfficial) : base(id, text, creationDate, creator, title, isOfficial)
        {
            this.Author = author;
            this.links = new List<string>(links);
        }

        public string[] GetLinks()
        {
            return links.ToArray();
        }
    }
}
