using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.User_classes
{
    public interface IUserRecommendationsSorter
    {
        ForumPublication[] SortRecommendations(Dictionary<ForumPublication, int> recommended_publications, int number);
    }
}
