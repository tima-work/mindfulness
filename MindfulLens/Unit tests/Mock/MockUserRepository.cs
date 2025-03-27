using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_tests.Mock
{
    public class MockUserRepository : IUserRepository
    {
        private Dictionary<User, int> watchedUsers;

        public MockUserRepository(Dictionary<User, int> watchedUsers)
        {
            this.watchedUsers = watchedUsers;
        }

        public void BanUser(User user)
        {
            throw new NotImplementedException();
        }

        public User[] GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Dictionary<User, int> GetAuthorsOfWatchedPublicationsByUser(User user)
        {
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword("MockUser123", salt);
            //return new Dictionary<User, int>()
            //{
            //    { new User(2, "Mock user 2", "mockuser2@gmail.com", hashedPassword, salt, "mockuser2", null, false, Importancy.User), 4 },
            //    { new User(3, "Mock user 3", "mockuser3@gmail.com", hashedPassword, salt, "mockuser3", null, false, Importancy.User), 2 },
            //    { new User(4, "Mock user 4", "mockuser4@gmail.com", hashedPassword, salt, "mockuser4", null, false, Importancy.User), 11 },
            //    { new User(5, "Mock user 5", "mockuser5@gmail.com", hashedPassword, salt, "mockuser5", null, false, Importancy.User), 1 },
            //    { new User(6, "Mock user 6", "mockuser6@gmail.com", hashedPassword, salt, "mockuser6", null, false, Importancy.User), 7 }
            //};
            return watchedUsers;
        }

        public User[] GetBannedUsers()
        {
            throw new NotImplementedException();
        }

        public User[] GetNotBannedUsers()
        {
            throw new NotImplementedException();
        }

        public User? GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByID(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, User> GetUserDictionaryForCreator(string sql, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, User> GetUserDictionaryForUser(string sql, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public void MakeAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public User? RegisterUser(User user)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
