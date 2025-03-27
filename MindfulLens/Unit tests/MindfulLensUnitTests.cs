using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Logic_Layer.Recommendations_creators.Keyword_classes;
using Logic_Layer.Recommendations_creators.User_classes;
using System.Runtime.Intrinsics.X86;
using Unit_tests.Mock;

namespace Unit_tests
{
    public class MindfulLensUnitTests
    {
        [Fact]
        public void CheckKeywordCalulation()
        {
            //Arrange

            MockUser2 user2 = new MockUser2();
            Dictionary<string, decimal> userKeywords = new Dictionary<string, decimal>
            {
                { "Technology", 3.65m },
                { "Robot", 7.378m },
                { "Science", 0.37m }
            };
            ForumPublication forumPublication1 = new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null);
            ForumPublication forumPublication2 = new ForumPublication(2, "Forum publication 2", DateTime.Now, user2, false, null);
            ForumPublication forumPublication3 = new ForumPublication(3, "Forum publication 3", DateTime.Now, user2, false, null);
            Dictionary<ForumPublication, Dictionary<string, decimal>> forumPublications = new Dictionary<ForumPublication, Dictionary<string, decimal>>
            {
                { forumPublication1, new Dictionary<string, decimal>
                    {
                        { "Technology", 5.5m },
                        { "Robot", 3.85m },
                        { "IT", 15.72m }
                    }
                },

                { forumPublication2, new Dictionary<string, decimal>
                    {
                        { "Technology", 13.74m },
                        { "IT", 1.52m },
                        { "Gadget", 3.72m }
                    }
                },

                { forumPublication3, new Dictionary<string, decimal>
                    {
                        { "Robot", 6.42m },
                        { "Research", 2.23m },
                        { "Science", 1.37m },
                        { "Electronics", 5.37m }
                    }
                }
            };
            MockForumRepository mockForumRepository = new MockForumRepository(userKeywords, forumPublications);
            KeywordCalculator keywordCalculator = new KeywordCalculator(mockForumRepository);

            //Act
            Dictionary<ForumPublication, decimal> calculatedPublications = keywordCalculator.CalculateKeywords(user2);

            //Assert
            Assert.Equal(calculatedPublications, new Dictionary<ForumPublication, decimal> 
            {
                { forumPublication1, 48.4803m },
                { forumPublication2, 50.151m },
                { forumPublication3, 47.87366m }
            });
        }


        [Fact]
        public void CheckKeywordSorting()
        {
            MockUser2 user2 = new MockUser2();
            KeywordRecommendationsSorter keywordRecommendationsSorter = new KeywordRecommendationsSorter();
            Dictionary<string, decimal> userKeywords = new Dictionary<string, decimal>
            {
                { "Technology", 3.65m },
                { "Robot", 7.378m },
                { "Science", 0.37m }
            };
            ForumPublication forumPublication1 = new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null);
            ForumPublication forumPublication2 = new ForumPublication(2, "Forum publication 2", DateTime.Now, user2, false, null);
            ForumPublication forumPublication3 = new ForumPublication(3, "Forum publication 3", DateTime.Now, user2, false, null);
            Dictionary<ForumPublication, Dictionary<string, decimal>> forumPublications = new Dictionary<ForumPublication, Dictionary<string, decimal>>
            {
                { forumPublication1, new Dictionary<string, decimal>
                    {
                        { "Technology", 5.5m },
                        { "Robot", 3.85m },
                        { "IT", 15.72m }
                    }
                },

                { forumPublication2, new Dictionary<string, decimal>
                    {
                        { "Technology", 13.74m },
                        { "IT", 1.52m },
                        { "Gadget", 3.72m }
                    }
                },

                { forumPublication3, new Dictionary<string, decimal>
                    {
                        { "Robot", 6.42m },
                        { "Research", 2.23m },
                        { "Science", 1.37m },
                        { "Electronics", 5.37m }
                    }
                }
            };
            MockForumRepository mockForumRepository = new MockForumRepository(userKeywords, forumPublications);
            KeywordCalculator keywordCalculator = new KeywordCalculator(mockForumRepository);
            Dictionary<ForumPublication, decimal> calculatedPublications = keywordCalculator.CalculateKeywords(user2);


            //Act
            ForumPublication[] recommendations = keywordRecommendationsSorter.SortRecommendations(calculatedPublications, 2);

            //Assert
            Assert.Equal(recommendations, new ForumPublication[2] { forumPublication2, forumPublication1 });
        }

        [Fact]
        public void CheckKeywordSortingWithZeroKeywords()
        {
            MockUser2 user2 = new MockUser2();
            KeywordRecommendationsSorter keywordRecommendationsSorter = new KeywordRecommendationsSorter();
            Dictionary<string, decimal> userKeywords = new Dictionary<string, decimal>
            {

            };
            ForumPublication forumPublication1 = new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null);
            ForumPublication forumPublication2 = new ForumPublication(2, "Forum publication 2", DateTime.Now, user2, false, null);
            ForumPublication forumPublication3 = new ForumPublication(3, "Forum publication 3", DateTime.Now, user2, false, null);
            Dictionary<ForumPublication, Dictionary<string, decimal>> forumPublications = new Dictionary<ForumPublication, Dictionary<string, decimal>>
            {
                { forumPublication1, new Dictionary<string, decimal>
                    {
                        { "Technology", 5.5m },
                        { "Robot", 3.85m },
                        { "IT", 15.72m }
                    }
                },

                { forumPublication2, new Dictionary<string, decimal>
                    {
                        { "Technology", 13.74m },
                        { "IT", 1.52m },
                        { "Gadget", 3.72m }
                    }
                },

                { forumPublication3, new Dictionary<string, decimal>
                    {
                        { "Robot", 6.42m },
                        { "Research", 2.23m },
                        { "Science", 1.37m },
                        { "Electronics", 5.37m }
                    }
                }
            };
            MockForumRepository mockForumRepository = new MockForumRepository(userKeywords, forumPublications);
            KeywordCalculator keywordCalculator = new KeywordCalculator(mockForumRepository);
            Dictionary<ForumPublication, decimal> calculatedPublications = keywordCalculator.CalculateKeywords(user2);


            //Act
            ForumPublication[] recommendations = keywordRecommendationsSorter.SortRecommendations(calculatedPublications, 5);

            //Assert
            Assert.Equal(recommendations, new ForumPublication[3] { forumPublication1, forumPublication2, forumPublication3 });
        }


        [Fact]
        public void CheckUserCalculation()
        {
            //IForumRepository forumRepository = new MockForumRepository();
            //IUserRepository userRepository = new MockUserRepository();
            //MockUser user = new MockUser();
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("MockUser123", salt);

            User user2 = new User(2, "Mock user 2", "mockuser2@gmail.com", hashedPassword, salt, "mockuser2", null, false, Importancy.User);
            User user3 = new User(3, "Mock user 3", "mockuser3@gmail.com", hashedPassword, salt, "mockuser3", null, false, Importancy.User);
            User user4 = new User(4, "Mock user 4", "mockuser4@gmail.com", hashedPassword, salt, "mockuser4", null, false, Importancy.User);
            User user5 = new User(5, "Mock user 5", "mockuser5@gmail.com", hashedPassword, salt, "mockuser5", null, false, Importancy.User);
            User user6 = new User(6, "Mock user 6", "mockuser6@gmail.com", hashedPassword, salt, "mockuser6", null, false, Importancy.User);
            User user7 = new User(7, "Mock user 7", "mockuser7@gmail.com", hashedPassword, salt, "mockuser7", null, false, Importancy.User);

            Dictionary<User, int> watchedUsers = new Dictionary<User, int>()
            {
                { user2, 4 },
                { user3, 2 },
                { user4, 11 },
                { user5, 1 },
                { user6, 7 }
            };

            ForumPublication forumPublication1 = new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null);
            ForumPublication forumPublication2 = new ForumPublication(2, "Forum publication 2", DateTime.Now, user5, false, null);
            ForumPublication forumPublication3 = new ForumPublication(3, "Forum publication 3", DateTime.Now, user6, false, null);
            ForumPublication forumPublication4 = new ForumPublication(4, "Forum publication 4", DateTime.Now, user2, false, null);
            ForumPublication forumPublication5 = new ForumPublication(5, "Forum publication 5", DateTime.Now, user3, false, null);
            ForumPublication forumPublication6 = new ForumPublication(6, "Forum publication 6", DateTime.Now, user7, false, null);
            ForumPublication forumPublication7 = new ForumPublication(7, "Forum publication 7", DateTime.Now, user4, false, null);
            ForumPublication forumPublication8 = new ForumPublication(8, "Forum publication 8", DateTime.Now, user3, false, null);

            ForumPublication[] notWatchedPublications = new ForumPublication[8]
            {
                forumPublication1,
                forumPublication2,
                forumPublication3,
                forumPublication4,
                forumPublication5,
                forumPublication6,
                forumPublication7,
                forumPublication8
            };
            MockForumRepository mockForumRepository = new MockForumRepository(notWatchedPublications);
            MockUserRepository mockUserRepository = new MockUserRepository(watchedUsers);
            UserCalculator userCalculator = new UserCalculator(mockUserRepository, mockForumRepository);

            //Act
            Dictionary<ForumPublication, int> calculatedPublications = userCalculator.CalculateUsers(user2);

            //Assert
            Assert.Equal(calculatedPublications, new Dictionary<ForumPublication, int>
            {
                { forumPublication1, 4 },
                { forumPublication2, 1 },
                { forumPublication3, 7 },
                { forumPublication4, 4 },
                { forumPublication5, 2 },
                { forumPublication6, 0 },
                { forumPublication7, 11 },
                { forumPublication8, 2 }
            });
        }


        [Fact]
        public void CheckUserSorting()
        {
            //Arrange
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("MockUser123", salt);

            User user2 = new User(2, "Mock user 2", "mockuser2@gmail.com", hashedPassword, salt, "mockuser2", null, false, Importancy.User);
            User user3 = new User(3, "Mock user 3", "mockuser3@gmail.com", hashedPassword, salt, "mockuser3", null, false, Importancy.User);
            User user4 = new User(4, "Mock user 4", "mockuser4@gmail.com", hashedPassword, salt, "mockuser4", null, false, Importancy.User);
            User user5 = new User(5, "Mock user 5", "mockuser5@gmail.com", hashedPassword, salt, "mockuser5", null, false, Importancy.User);
            User user6 = new User(6, "Mock user 6", "mockuser6@gmail.com", hashedPassword, salt, "mockuser6", null, false, Importancy.User);
            User user7 = new User(7, "Mock user 7", "mockuser7@gmail.com", hashedPassword, salt, "mockuser7", null, false, Importancy.User);

            Dictionary<User, int> watchedUsers = new Dictionary<User, int>()
            {
                { user2, 4 },
                { user3, 2 },
                { user4, 11 },
                { user5, 1 },
                { user6, 7 }
            };

            ForumPublication forumPublication1 = new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null);
            ForumPublication forumPublication2 = new ForumPublication(2, "Forum publication 2", DateTime.Now, user5, false, null);
            ForumPublication forumPublication3 = new ForumPublication(3, "Forum publication 3", DateTime.Now, user6, false, null);
            ForumPublication forumPublication4 = new ForumPublication(4, "Forum publication 4", DateTime.Now, user2, false, null);
            ForumPublication forumPublication5 = new ForumPublication(5, "Forum publication 5", DateTime.Now, user3, false, null);
            ForumPublication forumPublication6 = new ForumPublication(6, "Forum publication 6", DateTime.Now, user7, false, null);
            ForumPublication forumPublication7 = new ForumPublication(7, "Forum publication 7", DateTime.Now, user4, false, null);
            ForumPublication forumPublication8 = new ForumPublication(8, "Forum publication 8", DateTime.Now, user3, false, null);

            ForumPublication[] notWatchedPublications = new ForumPublication[8]
            {
                forumPublication1,
                forumPublication2,
                forumPublication3,
                forumPublication4,
                forumPublication5,
                forumPublication6,
                forumPublication7,
                forumPublication8
            };
            MockForumRepository mockForumRepository = new MockForumRepository(notWatchedPublications);
            MockUserRepository mockUserRepository = new MockUserRepository(watchedUsers);
            UserCalculator userCalculator = new UserCalculator(mockUserRepository, mockForumRepository);
            Dictionary<ForumPublication, int> calculatedPublications = userCalculator.CalculateUsers(user2);
            UserRecommendationsSorter userRecommendationsSorter = new UserRecommendationsSorter();

            //Act
            ForumPublication[] recommendations = userRecommendationsSorter.SortRecommendations(calculatedPublications, 5);

            //Assert
            Assert.Equal(recommendations, new ForumPublication[5] { notWatchedPublications[6], notWatchedPublications[2], notWatchedPublications[0], notWatchedPublications[3], notWatchedPublications[4] });
        }

        [Fact]
        public void CheckUserSortingWithZeroUsers()
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword("MockUser123", salt);

            User user2 = new User(2, "Mock user 2", "mockuser2@gmail.com", hashedPassword, salt, "mockuser2", null, false, Importancy.User);
            User user3 = new User(3, "Mock user 3", "mockuser3@gmail.com", hashedPassword, salt, "mockuser3", null, false, Importancy.User);
            User user4 = new User(4, "Mock user 4", "mockuser4@gmail.com", hashedPassword, salt, "mockuser4", null, false, Importancy.User);
            User user5 = new User(5, "Mock user 5", "mockuser5@gmail.com", hashedPassword, salt, "mockuser5", null, false, Importancy.User);
            User user6 = new User(6, "Mock user 6", "mockuser6@gmail.com", hashedPassword, salt, "mockuser6", null, false, Importancy.User);
            User user7 = new User(7, "Mock user 7", "mockuser7@gmail.com", hashedPassword, salt, "mockuser7", null, false, Importancy.User);

            Dictionary<User, int> watchedUsers = new Dictionary<User, int>() { };

            ForumPublication forumPublication1 = new ForumPublication(1, "Forum publication 1", DateTime.Now, user2, false, null);
            ForumPublication forumPublication2 = new ForumPublication(2, "Forum publication 2", DateTime.Now, user5, false, null);
            ForumPublication forumPublication3 = new ForumPublication(3, "Forum publication 3", DateTime.Now, user6, false, null);
            ForumPublication forumPublication4 = new ForumPublication(4, "Forum publication 4", DateTime.Now, user2, false, null);
            ForumPublication forumPublication5 = new ForumPublication(5, "Forum publication 5", DateTime.Now, user3, false, null);
            ForumPublication forumPublication6 = new ForumPublication(6, "Forum publication 6", DateTime.Now, user7, false, null);
            ForumPublication forumPublication7 = new ForumPublication(7, "Forum publication 7", DateTime.Now, user4, false, null);
            ForumPublication forumPublication8 = new ForumPublication(8, "Forum publication 8", DateTime.Now, user3, false, null);

            ForumPublication[] notWatchedPublications = new ForumPublication[8]
            {
                forumPublication1,
                forumPublication2,
                forumPublication3,
                forumPublication4,
                forumPublication5,
                forumPublication6,
                forumPublication7,
                forumPublication8
            };
            MockForumRepository mockForumRepository = new MockForumRepository(notWatchedPublications);
            MockUserRepository mockUserRepository = new MockUserRepository(watchedUsers);
            UserCalculator userCalculator = new UserCalculator(mockUserRepository, mockForumRepository);
            Dictionary<ForumPublication, int> calculatedPublications = userCalculator.CalculateUsers(user2);
            UserRecommendationsSorter userRecommendationsSorter = new UserRecommendationsSorter();

            //Act
            ForumPublication[] recommendations = userRecommendationsSorter.SortRecommendations(calculatedPublications, 5);

            //Assert
            Assert.Equal(recommendations, new ForumPublication[5] { notWatchedPublications[0], notWatchedPublications[1], notWatchedPublications[2], notWatchedPublications[3], notWatchedPublications[4] });

        }
    }
}