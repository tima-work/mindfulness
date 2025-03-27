using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators
{
    public interface IRecommendationsCreator
    {
        ForumPublication[] CreateRecommedations(User user, int number);
    }
}
