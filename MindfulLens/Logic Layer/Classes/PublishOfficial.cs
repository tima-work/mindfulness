using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class PublishOfficial
    {
        public int ID { get; private set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }
        public TitledPublication Publication { get; private set; }

        public PublishOfficial(TitledPublication publication, string text) : this(0, text, DateTime.Now, publication)
        {

        }

        public PublishOfficial(int id, string text, DateTime creationDate, TitledPublication publication)
        {
            ID = id;
            Text = text;
            CreationDate = creationDate;
            Publication = publication;
        }
    }
}
