 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string HashedPassword { get; private set; }
        public string Salt { get; private set; }
        public string Username { get; private set; }
        public byte[]? Photo {  get; private set; }
        public bool Banned { get; private set; }
        public Importancy Importancy { get; private set; }


        public User(string name, string email, string password, string username)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            PrivateConstructor(0, name, email, hashedPassword, salt, username, null, false, Importancy.User);
        }

        public User(int id, string name, string email, string hashedPassword, string salt, string username, byte[]? photo, bool banned, Importancy importancy)
        {
            PrivateConstructor(id, name, email, hashedPassword, salt, username, photo, banned, importancy);
        }

        private void PrivateConstructor(int id, string name, string email, string hashedPassword, string salt, string username, byte[]? photo, bool banned, Importancy importancy)
        {
            Id = id;
            Name = name;
            Email = email;
            HashedPassword = hashedPassword;
            Salt = salt;
            Username = username;
            Photo = photo;
            Banned = banned;
            Importancy = importancy;
        }
    }
}
