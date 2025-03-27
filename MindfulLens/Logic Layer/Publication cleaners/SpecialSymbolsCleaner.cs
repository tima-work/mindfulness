using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Publication_cleaners
{
    public class SpecialSymbolsCleaner : IPublicationCleaner
    {
        public string MakeTextCleaner(string text)
        {
            string cleaner_text = text;
            char[] chars_to_remove = new char[] { ',', '.', '?', '!', ':', ';', '(', ')', '\"', '@', '<', '>', '#', '[', ']', '\\', '_', '=', '+', '*', '{', '}' };
            foreach (char c in chars_to_remove)
            {
                cleaner_text = cleaner_text.Replace(c, ' ');
            }
            return cleaner_text;
        }
    }
}
