using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Custom_exceptions
{
    public class WrongInputException : Exception
    {
        public WrongInputException(string? message) : base(message) { }
    }
}
