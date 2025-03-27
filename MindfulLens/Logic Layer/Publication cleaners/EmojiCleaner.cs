using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Layer.Publication_cleaners
{
    public class EmojiCleaner : IPublicationCleaner
    {
        public string MakeTextCleaner(string text)
        {
            return Regex.Replace(text, @"\uD83D[\uDC00-\uDFFF]|\uD83C[\uDC00-\uDFFF]|\uFFFD", "");
        }
    }
}
