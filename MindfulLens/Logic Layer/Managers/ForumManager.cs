using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Logic_Layer.Keywords_finders;
using Logic_Layer.Publication_cleaners;
using Logic_Layer.Recommendations_creators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class ForumManager
    {
        private IForumRepository forumRepository;
        private IKeywordsFinder keywordsFinder;

        public ForumManager(IForumRepository forumRepository)
        {
            keywordsFinder = new GraphTextRankKeywordsFinder(new IPublicationCleaner[] { new FunctionalPartsCleaner(), new SpecialSymbolsCleaner(), new EmojiCleaner() });
            this.forumRepository = forumRepository;
        }


        public void AddForumPublication(ForumPublication forumPublication)
        {
            CheckForumPublicationInfo(forumPublication);
            int id = forumRepository.AddForumPublication(forumPublication);
            ForumPublication newForumPublication = new ForumPublication(id, forumPublication.Text, forumPublication.CreationDate, forumPublication.Creator, forumPublication.IsDeleted, forumPublication.QuestionPublication);
            forumRepository.AddKeywords(keywordsFinder.FindKeywords(forumPublication.Text), newForumPublication);
        }


        public ForumPublicationWithReplies[] GetQuestionPublications(string? search_query, int number)
        {
            return forumRepository.GetQuestionForumPublications(search_query, number);
        }
        //public ForumPublicationWithReplies[] GetReccomendations(User user, int number)
        //{
        //    Keyword[] keywords = forumRepository.GetKeywordsByUser(user);
        //    Dictionary<string, double> keywords_dict = keywordsCalculator.CalculateKeywords(keywords);
        //    Dictionary<ForumPublication, Dictionary<string, double>> forumPublications = forumRepository.GetForumPublicationsWithKeywords();
        //    ForumPublication[] recommended_publications = recommendationsCreator.GetRecommendations(forumPublications, keywords_dict, number);
        //    return forumRepository.CastToPublicationsWithReplies(recommended_publications);



        //}


        public ForumPublication[] GetBranch(ForumPublication questionPublication, int number)
        {
            //List<ForumPublication> past_list = new List<ForumPublication>();
            //List<ForumPublication> current_list = new List<ForumPublication>() { questionPublication };
            //List<ForumPublication> future_list = new List<ForumPublication>();
            ////List<ForumPublication> all_list = new List<ForumPublication>(forumRepository.GetAllForumPublications());
            //while (current_list.Count > 0)
            //{
            //    foreach (ForumPublication forumPublication in current_list)
            //    {

            //        if (forumPublication.QuestionPublication != null && current_list.FirstOrDefault(f => f.ID == forumPublication.QuestionPublication.ID) != null)
            //        {
            //            future_list.Add(forumPublication);
            //        }
            //    }


            //    past_list.AddRange(current_list);
            //    current_list.Clear();
            //    current_list.AddRange(future_list);
            //    foreach (ForumPublication forumPublication in future_list)
            //        all_list.Remove(forumPublication);
            //    future_list.Clear();
            //}
            //return past_list.ToArray();
            return forumRepository.GetBranch(questionPublication, number);
        }

        public void DeleteForumPublication(ForumPublication forumPublication)
        {
            forumRepository.DeleteForumPublication(forumPublication);
        }

        public void UpdateForumPublication(ForumPublication forumPublication)
        {
            CheckForumPublicationInfo(forumPublication);
            forumRepository.DeleteKeywordsByForumPublication(forumPublication);
            forumRepository.UpdateForumPublication(forumPublication);
            forumRepository.AddKeywords(keywordsFinder.FindKeywords(forumPublication.Text), forumPublication);
        }

        private void CheckForumPublicationInfo(ForumPublication forumPublication)
        {
            if (String.IsNullOrWhiteSpace(forumPublication.Text))
                throw new Exception("You haven't entered text of publication");
        }

        public ForumPublication[] GetForumPublicationsByCreator(User creator)
        {
            return forumRepository.GetForumPublicationsByCreator(creator);
        }

        public void WatchPublication(ForumPublication forumPublication, User user)
        {
            forumRepository.AddPublicationToWatchHistory(user, forumPublication);
        }

        public ForumPublication? GetForumPublicationById(int id)
        {
            return forumRepository.GetForumPublicationById(id);
        }

        //public Dictionary<ForumPublication, int> SearchForQuestionForumPublications(string query, int number)
        //{
        //    return forumRepository.SearchForQuestionForumPublications(query, number);
        //}

        public ForumPublicationWithReplies[] CreateRecommendations(IRecommendationsCreator creator, User user, int number)
        {
            return forumRepository.CastToPublicationsWithReplies(creator.CreateRecommedations(user, number));
        }
    }
}
