using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Publication_cleaners
{
    public interface IPublicationCleaner
    {
        string MakeTextCleaner(string text);
    }
}
