using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IUserRepository
    {
        User[] GetAllUsers();
        User[] GetBannedUsers();
        User[] GetNotBannedUsers();
        //Dictionary<int, User> GetUserDictionary(string sql, object parameters = null);
        Dictionary<int, User> GetUserDictionaryForUser(string sql, object parameters = null);
        Dictionary<int, User> GetUserDictionaryForCreator(string sql, object parameters = null);
        Dictionary<User, int> GetAuthorsOfWatchedPublicationsByUser(User user);
        User? GetUserByID(int id);
        User? GetUserByEmail(string email);
        User? GetUserByUsername(string username);
        User? RegisterUser(User user);
        void UpdateUser(User user);
        void BanUser(User user);
        void ResetPassword(User user);
        void MakeAdmin(User user);
    }
}
