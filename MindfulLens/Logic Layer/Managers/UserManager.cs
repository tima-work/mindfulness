using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class UserManager
    {
        private IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User? Register(User user, string unhashedPassword)
        {
            CheckUserInfo(user, false);
            CheckUserPassword(unhashedPassword);
            return userRepository.RegisterUser(user);
        }


        public User? Login(string email, string password)
        {
            User? found_user = userRepository.GetUserByEmail(email);
            if (found_user != null && BCrypt.Net.BCrypt.HashPassword(password, found_user.Salt) == found_user.HashedPassword)
                return found_user;
            return null;
        }


        public User[] GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public void BanUser(User user)
        {
            userRepository.BanUser(user);
        }

        public void UpdateUser(User user)
        {
            CheckUserInfo(user, true);
            userRepository.UpdateUser(user);
        }

        public void ResetPassword(User user, string unhashedPassword)
        {
            CheckUserPassword(unhashedPassword);
            userRepository.ResetPassword(user);
        }

        private void CheckUserInfo(User user, bool checkById)
        {
            if (String.IsNullOrWhiteSpace(user.Name))
                throw new WrongInputException("You haven't entered name");
            if (String.IsNullOrWhiteSpace(user.Email))
                throw new WrongInputException("You haven't entered email");
            if (String.IsNullOrWhiteSpace(user.Username))
                throw new WrongInputException("You haven't entered username");
            User? found_user = userRepository.GetUserByEmail(user.Email);
            User? found_user2 = userRepository.GetUserByUsername(user.Username);
            if (found_user != null && (!checkById || found_user.Id != user.Id))
                throw new WrongInputException("User with such email is already registered");
            if (found_user2 != null && (!checkById || found_user2.Id != user.Id))
                throw new WrongInputException("This username is already taken");
        }

        private void CheckUserPassword(string unhashedPassword)
        {
            if (unhashedPassword.Length < 8 || !Regex.IsMatch(unhashedPassword, "[A-Z]+") || !Regex.IsMatch(unhashedPassword, "[a-z]+") || !Regex.IsMatch(unhashedPassword, "[0-9]+") || !Regex.IsMatch(unhashedPassword, "[^A-Za-z0-9]+"))
                throw new WrongInputException("The password must be at least 8 characters long, it must contain uppercase letter, lowercase letter, a number and a special symbol");
        }

        public User[] GetBannedUsers()
        {
            return userRepository.GetBannedUsers();
        }

        public User[] GetNotBannedUsers()
        {
            return userRepository.GetNotBannedUsers();
        }

        public User? GetUserById(int id)
        {
            return userRepository.GetUserByID(id);
        }
    }
}
