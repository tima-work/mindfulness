using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Logic_Layer.Recommendations_creators.User_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators
{
    public class OtherUsersRecommendationsCreator : IRecommendationsCreator
    {
        private IForumRepository forumRepository;
        private IUserRepository userRepository;
        private IUserCalculator userCalculator;
        private IUserRecommendationsSorter userRecommendationsSorter;

        public OtherUsersRecommendationsCreator(IForumRepository forumRepository, IUserRepository userRepository, IUserCalculator userCalculator, IUserRecommendationsSorter userRecommendationsSorter)
        {
            this.forumRepository = forumRepository;
            this.userRepository = userRepository;
            this.userCalculator = userCalculator;
            this.userRecommendationsSorter = userRecommendationsSorter;
        }

        public ForumPublication[] CreateRecommedations(User user, int number)
        {
            Dictionary<ForumPublication, int> recommended_publications = userCalculator.CalculateUsers(user);

            return userRecommendationsSorter.SortRecommendations(recommended_publications, number);
            
            //throw new Exception("Aboba");
            //return list_to_return;
        }
    }
}
