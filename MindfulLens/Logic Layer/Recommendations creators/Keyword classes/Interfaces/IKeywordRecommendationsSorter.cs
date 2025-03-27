using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.Keyword_classes
{
    public interface IKeywordRecommendationsSorter
    {
        ForumPublication[] SortRecommendations(Dictionary<ForumPublication, decimal> ranked_publications, int number);
    }
}
