using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.User_classes
{
    public class UserRecommendationsSorter : IUserRecommendationsSorter
    {
        public ForumPublication[] SortRecommendations(Dictionary<ForumPublication, int> recommended_publications, int number)
        {
            return recommended_publications
                .OrderByDescending(r => r.Value)
                .Take(number)
                .Select(r => r.Key)
                .ToArray();
        }
    }
}
