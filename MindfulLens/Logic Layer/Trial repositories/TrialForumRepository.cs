using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer
{
    public class TrialForumRepository : IForumRepository
    {
        private List<ForumPublication> publications;
        public TrialForumRepository()
        {
            User user1 = new User("Bob", "bob@gmail.com", "bob12345", "bob");
            User creator1 = new User(1, user1.Name, user1.Email, user1.HashedPassword, user1.Salt, user1.Username, user1.Photo, user1.Banned, user1.Importancy);
            publications = new List<ForumPublication>()
            {
                new ForumPublication(1, "Publication 1", DateTime.Now, creator1, false, null),
                new ForumPublication(2, "Publication 2", DateTime.Now, creator1, false, null),
                new ForumPublication(3, "Publication 3", DateTime.Now, creator1, false, null),
                new ForumPublication(4, "Publication 4", DateTime.Now, creator1, false, null),
                new ForumPublication(9, "Publication 9", DateTime.Now, creator1, false, null),
                new ForumPublication(10, "Publication 10", DateTime.Now, creator1, false, null),
                new ForumPublication(11, "Publication 11", DateTime.Now, creator1, false, null),
                new ForumPublication(12, "Publication 12", DateTime.Now, creator1, false, null),
                new ForumPublication(13, "Publication 13", DateTime.Now, creator1, false, null),
                new ForumPublication(14, "Publication 14", DateTime.Now, creator1, false, null),
                new ForumPublication(15, "Publication 15", DateTime.Now, creator1, false, null),
                new ForumPublication(16, "Publication 16", DateTime.Now, creator1, false, null),
                new ForumPublication(17, "Publication 17", DateTime.Now, creator1, false, null),
                new ForumPublication(18, "Publication 18", DateTime.Now, creator1, false, null),
                new ForumPublication(19, "Publication 19", DateTime.Now, creator1, false, null),
                new ForumPublication(20, "Publication 20", DateTime.Now, creator1, false, null),
                new ForumPublication(21, "Publication 21", DateTime.Now, creator1, false, null),
                new ForumPublication(22, "Publication 22", DateTime.Now, creator1, false, null),
                new ForumPublication(23, "Publication 23", DateTime.Now, creator1, false, null),
                new ForumPublication(24, "Publication 24", DateTime.Now, creator1, false, null),
                new ForumPublication(25, "Publication 25", DateTime.Now, creator1, false, null),
                new ForumPublication(26, "Publication 26", DateTime.Now, creator1, false, null),
                new ForumPublication(27, "Publication 27", DateTime.Now, creator1, false, null),
                new ForumPublication(28, "Publication 28", DateTime.Now, creator1, false, null),
                new ForumPublication(29, "Publication 29", DateTime.Now, creator1, false, null),
                new ForumPublication(30, "Publication 30", DateTime.Now, creator1, false, null),
            };

            publications.Add(new ForumPublication(5, "Publication 5", DateTime.Now, creator1, false, publications[0]));
            publications.Add(new ForumPublication(6, "Publication 6", DateTime.Now, creator1, false, publications[4]));
            publications.Add(new ForumPublication(7, "Publication 7", DateTime.Now, creator1, false, publications[0]));
            publications.Add(new ForumPublication(8, "Publication 8", DateTime.Now, creator1, false, publications[5]));
        }
        public int AddForumPublication(ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public void AddKeywords(string[] keywords, ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public void AddPublicationToWatchHistory(User user, ForumPublication forumPublication)
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

        public ForumPublication[] GetAllForumPublications(int number)
        {
            return publications.Take(number).ToArray();
        }

        public ForumPublication[] GetBranch(ForumPublication questionPublication, int number)
        {
            List<ForumPublication> past_list = new List<ForumPublication>();
            List<ForumPublication> current_list = new List<ForumPublication>() { questionPublication };
            List<ForumPublication> future_list = new List<ForumPublication>();
            List<ForumPublication> all_list = new List<ForumPublication>(publications);

            while (current_list.Count > 0)
            {
                foreach (ForumPublication forumPublication in all_list)
                {
                    if (forumPublication.QuestionPublication != null && current_list.FirstOrDefault(f => f.ID == forumPublication.QuestionPublication.ID) != null)
                    {
                        future_list.Add(forumPublication);
                    }
                }

                past_list.AddRange(current_list);
                current_list.Clear();
                foreach (ForumPublication forumPublication in future_list)
                    all_list.Remove(forumPublication);
                current_list.AddRange(future_list);
                future_list.Clear();
            }

            return past_list.Take(number).ToArray();
        }
        
        public ForumPublication? GetForumPublicationById(int id)
        {
            return publications.FirstOrDefault(p => p.ID == id);
        }

        public ForumPublication[] GetForumPublicationsByCreator(User user)
        {
            throw new NotImplementedException();
        }

        public string[] GetKeywordsByUser(User user)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ForumPublication, int> GetQuestionForumPublications(int number)
        {
            return GetForumPublicationsWithReply(publications.Where(p => p.QuestionPublication == null).Take(number).ToArray());
        }

        public Dictionary<ForumPublication, int> SearchForQuestionForumPublications(string query, int number)
        {
            return GetForumPublicationsWithReply(publications.Where(f => f.Text.Contains(query)).Take(number).ToArray());
        }



        private Dictionary<ForumPublication, int> GetForumPublicationsWithReply(ForumPublication[] current_publications)
        {
            int[] replies = new int[current_publications.Length];
            List<int>[] current_lists = new List<int>[current_publications.Length];
            List<int>[] future_lists = new List<int>[current_publications.Length];
            bool continue_loop = true;

            for (int i = 0; i < current_publications.Length; i++)
            {
                current_lists[i] = new List<int> { current_publications[i].ID };
                future_lists[i] = new List<int>();
                replies[i] = 0;
            }

            while (continue_loop)
            {
                continue_loop = false;

                for (int i = 0; i < current_lists.Length; i++)
                {
                    if (current_lists[i].Count > 0)
                    {
                        //foreach (ForumPublication forumPublication in publications)
                        //{
                        //    if (forumPublication.QuestionPublication != null && current_lists[i].Contains(forumPublication.QuestionPublication.ID))
                        //        future_lists[i].Add(forumPublication.ID);
                        //}
                        future_lists[i].AddRange(publications.Where(p => p.QuestionPublication != null && current_lists[i].Contains(p.QuestionPublication.ID)).Select(item => item.ID));
                        replies[i] += current_lists[i].Count;
                        current_lists[i].Clear();
                        current_lists[i].AddRange(future_lists[i]);
                        future_lists[i].Clear();
                        continue_loop = true;
                    }
                }
            }

            Dictionary<ForumPublication, int> dict_to_return = new Dictionary<ForumPublication, int>();

            for (int i = 0; i < current_publications.Length; i++)
            {
                dict_to_return.Add(current_publications[i], replies[i] - 1);
            }

            return dict_to_return;
        }

        public void UpdateForumPublication(ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        public void AddKeywords(Keyword[] keyWords, ForumPublication forumPublication)
        {
            throw new NotImplementedException();
        }

        //public Dictionary<string, int> GetKeywordsByUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        public Dictionary<ForumPublication, Dictionary<string, double>> GetForumPublicationsWithKeywords()
        {
            throw new NotImplementedException();
        }

        public Dictionary<ForumPublication, int> CastToDictionaryWithReplies(ForumPublication[] publications)
        {
            throw new NotImplementedException();
        }

        public ForumPublicationWithReplies[] GetQuestionForumPublications(string? search_query, int number)
        {
            throw new NotImplementedException();
        }

        public ForumPublicationWithReplies[] CastToPublicationsWithReplies(ForumPublication[] publications)
        {
            throw new NotImplementedException();
        }

        public ForumPublicationWithReplies[] GetNotWatchedPublications(User current_user)
        {
            throw new NotImplementedException();
        }

        public Dictionary<ForumPublication, Dictionary<string, double>> GetNotWatchedPublicationsWithKeywords(User user)
        {
            throw new NotImplementedException();
        }

        ForumPublication[] IForumRepository.GetNotWatchedPublications(User current_user)
        {
            throw new NotImplementedException();
        }

        Dictionary<string, decimal> IForumRepository.GetKeywordsByUser(User user)
        {
            throw new NotImplementedException();
        }

        Dictionary<ForumPublication, Dictionary<string, decimal>> IForumRepository.GetNotWatchedPublicationsWithKeywords(User user)
        {
            throw new NotImplementedException();
        }
    }
}
