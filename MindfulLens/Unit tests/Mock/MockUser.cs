using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_tests.Mock
{
    public class MockUser : User
    {
        private static string salt = BCrypt.Net.BCrypt.GenerateSalt();
        public MockUser() : base(1, "Mock user", "mockuser@gmail.com", BCrypt.Net.BCrypt.HashPassword("MockUser123", salt), salt, "MockUser", null, false, Importancy.User)
        {

        }
    }
}
