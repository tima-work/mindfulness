using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Custom_exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string? message) : base("Sorry, we have problem with the database: " + message) { }
        public DatabaseException() : base("Sorry, we have some problem with the database") { }
    }
}
