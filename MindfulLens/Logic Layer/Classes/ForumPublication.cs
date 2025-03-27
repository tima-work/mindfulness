using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class ForumPublication : Publication
    {
        public bool IsDeleted {  get; private set; }
        public ForumPublication? QuestionPublication { get; private set; }

        public ForumPublication(string text, User creator) : this(text, creator, null) { }

        public ForumPublication(string text, User creator, ForumPublication? questionPublication) : this(0, text, DateTime.Now, creator, false, questionPublication)
        {

        }

        public ForumPublication(int id, string text, DateTime creationDate, User creator, bool isDeleted, ForumPublication? questionPublication) : base(id, text, creationDate, creator)
        {
            this.IsDeleted = isDeleted;
            this.QuestionPublication = questionPublication;
        }
    }
}
