using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Custom_exceptions
{
    public class BannedUserException : Exception
    {
        public BannedUserException() : base("Sorry, your were banned") { }
    }
}
