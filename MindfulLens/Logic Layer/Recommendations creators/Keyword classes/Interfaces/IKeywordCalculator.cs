using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.Keyword_classes
{
    public interface IKeywordCalculator
    {
        public Dictionary<ForumPublication, decimal> CalculateKeywords(User user);

    }
}
