using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Logic_Layer.Recommendations_creators.Keyword_classes
{
    public class KeywordRecommendationsSorter : IKeywordRecommendationsSorter
    {
        public ForumPublication[] SortRecommendations(Dictionary<ForumPublication, decimal> ranked_publications, int number)
        {
            return ranked_publications
                 .OrderByDescending(kvp => kvp.Value)
                 .Take(number)
                 .Select(k => k.Key)
                 .ToArray();
        }
    }
}
