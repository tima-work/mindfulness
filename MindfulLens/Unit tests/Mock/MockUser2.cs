using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_tests.Mock
{
    public class MockUser2 : User
    {
        private static string salt = BCrypt.Net.BCrypt.GenerateSalt();
        public MockUser2() : base(2, "Mock user 2", "mockuser2@gmail.com", BCrypt.Net.BCrypt.HashPassword("MockUser123", salt), salt, "MockUser2", null, false, Importancy.User)
        {

        }
    }
}
