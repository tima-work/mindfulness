using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.User_classes
{
    public class UserCalculator : IUserCalculator
    {
        private IUserRepository userRepository;
        private IForumRepository forumRepository;

        public UserCalculator(IUserRepository userRepository, IForumRepository forumRepository)
        {
            this.userRepository = userRepository;
            this.forumRepository = forumRepository;
        }

        private Dictionary<ForumPublication, int> CalculateUsers(Dictionary<User, int> watched_users, ForumPublication[] forumPublications)
        {
            Dictionary<ForumPublication, int> recommended_publications = new Dictionary<ForumPublication, int>();

            foreach (ForumPublication forumPublication in forumPublications)
            {
                User? author = watched_users.FirstOrDefault(u => u.Key.Id == forumPublication.Creator.Id).Key;
                if (author != null)
                    recommended_publications[forumPublication] = watched_users[author];
                else
                    recommended_publications[forumPublication] = 0;
            }
            return recommended_publications;
        }

        public Dictionary<ForumPublication, int> CalculateUsers(User user)
        {
            return CalculateUsers(userRepository.GetAuthorsOfWatchedPublicationsByUser(user), forumRepository.GetNotWatchedPublications(user));
        }
    }
}
