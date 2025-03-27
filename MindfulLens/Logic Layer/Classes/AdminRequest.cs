using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class AdminRequest : Publication
    {
        public bool IsOffer {  get; private set; }
        public AdminRequest(string text, User creator, bool isOffer) : this(0, text, DateTime.Now, creator, isOffer)
        {

        }

        public AdminRequest(int id, string text, DateTime creationDate, User creator, bool isOffer) : base(id, text, creationDate, creator)
        {
            IsOffer = isOffer;
        }
    }
}
