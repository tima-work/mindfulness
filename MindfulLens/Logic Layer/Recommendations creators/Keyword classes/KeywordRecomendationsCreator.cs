using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Logic_Layer.Recommendations_creators.Keyword_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators
{
    public class KeywordRecomendationsCreator : IRecommendationsCreator
    {
        private IForumRepository forumRepository;
        private IKeywordCalculator keywordCalculator;
        private IKeywordRecommendationsSorter keywordRecommendationsSorter;
        public KeywordRecomendationsCreator(IForumRepository forumRepository, IKeywordCalculator keywordCalculator, IKeywordRecommendationsSorter keywordRecommendationsSorter)
        {
            this.forumRepository = forumRepository;
            this.keywordCalculator = keywordCalculator;
            this.keywordRecommendationsSorter = keywordRecommendationsSorter;
        }

        public ForumPublication[] CreateRecommedations(User user, int number)
        {
            Dictionary<ForumPublication, decimal> ranked_publications = keywordCalculator.CalculateKeywords(user);

            return keywordRecommendationsSorter.SortRecommendations(ranked_publications, number);

            //return recommendations;
            //return forumRepository.CastToPublicationsWithReplies(recommendations);
        }
    }
}
