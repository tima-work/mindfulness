using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;


namespace Logic_Layer.Interfaces
{
    public interface IForumRepository
    {
        ForumPublicationWithReplies[] GetQuestionForumPublications(string? search_query, int number);
        ForumPublication[] GetForumPublicationsByCreator(User user);
        ForumPublication[] GetBranch(ForumPublication questionPublication, int number);
        //ForumPublicationWithReplies[] SearchForQuestionForumPublications(string query, int number);
        ForumPublication? GetForumPublicationById(int id);
        int AddForumPublication(ForumPublication forumPublication);
        void UpdateForumPublication(ForumPublication forumPublication);
        void DeleteForumPublication(ForumPublication forumPublication);
        void AddKeywords(Keyword[] keyWords, ForumPublication forumPublication);
        void DeleteKeywordsByForumPublication(ForumPublication forumPublication);
        Dictionary<string, decimal> GetKeywordsByUser(User user);
        void AddPublicationToWatchHistory(User user, ForumPublication forumPublication);
        Dictionary<ForumPublication, Dictionary<string, decimal>> GetNotWatchedPublicationsWithKeywords(User user);
        ForumPublicationWithReplies[] CastToPublicationsWithReplies(ForumPublication[] publications);
        ForumPublication[] GetNotWatchedPublications(User current_user);
    }
}
