using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Keywords_finders
{
    public interface IKeywordsFinder
    {
        Keyword[] FindKeywords(string text);
    }
}
