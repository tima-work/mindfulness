using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_tests.Mock
{
    public class MockForumRepository : IForumRepository
    {
        private Dictionary<string, decimal> userKeywords;
        private Dictionary<ForumPublication, Dictionary<string, decimal>> forumPublications;


        private ForumPublication[] notWatchedPublications;


        public MockForumRepository(Dictionary<string, decimal> userKeywords, Dictionary<ForumPublication, Dictionary<string, decimal>> forumPublications)
        {
            this.userKeywords = userKeywords;
            this.forumPublications = forumPublications;
        }

        public MockForumRepository(ForumPublication[] notWatchedPublications)
        {
            this.notWatchedPublications = notWatchedPublications;
        }


        public int AddForumPublication(ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public void AddKeywords(Keyword[] keyWords, ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public void AddPublicationToWatchHistory(User user, ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public ForumPublicationWithReplies[] CastToPublicationsWithReplies(ForumPublication[] publications)
        {
            throw new NotImplementedException();
        }

        public void DeleteForumPublication(ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public void DeleteKeywordsByForumPublication(ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public ForumPublication[] GetBranch(ForumPublication questionPublication, int number)
        {
            throw new NotImplementedException();
        }

        public ForumPublication? GetForumPublicationById(int id)
        {
            throw new NotImplementedException();
        }

        public ForumPublication[] GetForumPublicationsByCreator(User user)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, decimal> GetKeywordsByUser(User user)
        {
            //return new Dictionary<string, decimal>
            //{
            //    { "Technology", 3.65m },
            //    { "Robot", 7.378m },
            //    { "Science", 0.37m }
            //};
            return userKeywords;
        }

        public ForumPublication[] GetNotWatchedPublications(User current_user)
        {
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword("MockUser123", salt);

            //User user2 = new User(2, "Mock user 2", "mockuser2@gmail.com", hashedPassword, salt, "mockuser2", null, false, Importancy.User);
            //User user3 = new User(3, "Mock user 3", "mockuser3@gmail.com", hashedPassword, salt, "mockuser3", null, false, Importancy.User);
            //User user4 = new User(4, "Mock user 4", "mockuser4@gmail.com", hashedPassword, salt, "mockuser4", null, false, Importancy.User);
            //User user5 = new User(5, "Mock user 5", "mockuser5@gmail.com", hashedPassword, salt, "mockuser5", null, false, Importancy.User);
            //User user6 = new User(6, "Mock user 6", "mockuser6@gmail.com", hashedPassword, salt, "mockuser6", null, false, Importancy.User);
            //User user7 = new User(7, "Mock user 7", "mockuser7@gmail.com", hashedPassword, salt, "mockuser7", null, false, Importancy.User);

            //return new ForumPublication[8]
            //{
            //    new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null),
            //    new ForumPublication(2, "Forum publication 2", DateTime.Now, user5, false, null),
            //    new ForumPublication(3, "Forum publication 3", DateTime.Now, user6, false, null),
            //    new ForumPublication(4, "Forum publication 4", DateTime.Now, user2, false, null),
            //    new ForumPublication(5, "Forum publication 5", DateTime.Now, user3, false, null),
            //    new ForumPublication(6, "Forum publication 6", DateTime.Now, user7, false, null),
            //    new ForumPublication(7, "Forum publication 7", DateTime.Now, user4, false, null),
            //    new ForumPublication(8, "Forum publication 8", DateTime.Now, user3, false, null),
            //};
            return notWatchedPublications;
        }

        public Dictionary<ForumPublication, Dictionary<string, decimal>> GetNotWatchedPublicationsWithKeywords(User user)
        {
            //User user2 = new MockUser2();
            //return new Dictionary<ForumPublication, Dictionary<string, decimal>>
            //{
            //    { new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null), new Dictionary<string, decimal>
            //        {
            //            { "Technology", 5.5m },
            //            { "Robot", 3.85m },
            //            { "IT", 15.72m }
            //        }
            //    },

            //    { new ForumPublication(2, "Forum publication 2", DateTime.Now, user2, false, null), new Dictionary<string, decimal>
            //        {
            //            { "Technology", 13.74m },
            //            { "IT", 1.52m },
            //            { "Gadget", 3.72m }
            //        }
            //    },

            //    { new ForumPublication(3, "Forum publication 3", DateTime.Now, user2, false, null), new Dictionary<string, decimal>
            //        {
            //            { "Robot", 6.42m },
            //            { "Research", 2.23m },
            //            { "Science", 1.37m },
            //            { "Electronics", 5.37m }
            //        }
            //    },
            //};
            return forumPublications;
        }

        public ForumPublicationWithReplies[] GetQuestionForumPublications(string? search_query, int number)
        {
            throw new NotImplementedException();
        }

        public void UpdateForumPublication(ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }
    }
}
