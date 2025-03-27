using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.Keyword_classes
{
    public class KeywordCalculator : IKeywordCalculator
    {
        private IForumRepository forumRepository;

        public KeywordCalculator(IForumRepository forumRepository)
        {
            this.forumRepository = forumRepository;
        }

        private Dictionary<ForumPublication, decimal> CalculateKeywords(Dictionary<string, decimal> userKeywords, Dictionary<ForumPublication, Dictionary<string, decimal>> forumPublications)
        {
            Dictionary<ForumPublication, decimal> ranked_publications = new Dictionary<ForumPublication, decimal>();

            foreach (KeyValuePair<ForumPublication, Dictionary<string, decimal>> kvp in forumPublications)
            {
                ranked_publications[kvp.Key] = 0.00m;


                foreach (KeyValuePair<string, decimal> kvp2 in kvp.Value)
                {
                    if (userKeywords.TryGetValue(kvp2.Key, out decimal value2))
                    {
                        ranked_publications[kvp.Key] += kvp2.Value * value2;
                    }
                }
            }
            return ranked_publications;
        }

        public Dictionary<ForumPublication, decimal> CalculateKeywords(User user)
        {
            return CalculateKeywords(forumRepository.GetKeywordsByUser(user), forumRepository.GetNotWatchedPublicationsWithKeywords(user));
        }
    }
}
