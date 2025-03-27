using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class ForumPublicationWithReplies
    {
        public ForumPublication forumPublication {  get; private set; }
        public int reply_number { get; private set; }

        public ForumPublicationWithReplies(ForumPublication forumPublication, int reply_number)
        {
            this.forumPublication = forumPublication;
            this.reply_number = reply_number;
        }
    }
}
