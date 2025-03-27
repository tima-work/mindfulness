using Logic_Layer.Interfaces;
using Logic_Layer.Recommendations_creators;
using Logic_Layer.Recommendations_creators.Keyword_classes;
using Logic_Layer.Recommendations_creators.User_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Factories
{
    public class AlgorithmFactory
    {
        private Dictionary<string, IRecommendationsCreator> association_dict;

        public AlgorithmFactory(IForumRepository forumRepository, IUserRepository userRepository)
        {
            KeywordCalculator keywordCalculator = new KeywordCalculator(forumRepository);
            KeywordRecommendationsSorter keywordRecommendationsSorter = new KeywordRecommendationsSorter();

            UserCalculator userCalculator = new UserCalculator(userRepository, forumRepository);
            UserRecommendationsSorter userRecommendationsSorter = new UserRecommendationsSorter();
            association_dict = new Dictionary<string, IRecommendationsCreator>
            {
                { "keywords", new KeywordRecomendationsCreator(forumRepository, keywordCalculator, keywordRecommendationsSorter) },
                { "users", new OtherUsersRecommendationsCreator(forumRepository, userRepository, userCalculator, userRecommendationsSorter) }
            };
        }

        public IRecommendationsCreator? GetRecommendationsCreator(string? word)
        {
            if (word != null && association_dict.ContainsKey(word)) {
                return association_dict[word];
            }
            return null;
        }
    }
}
