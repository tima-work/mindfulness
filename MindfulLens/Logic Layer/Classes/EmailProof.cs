using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Classes
{
    public class EmailProof
    {
        public int Id { get; private set; }
        public string Email { get; private set; }
        public int Code { get; private set; }
        public DateTime CreationTime {  get; private set; }

        public EmailProof(int id, string email, int code, DateTime creationTime)
        {
            Id = id;
            Email = email;
            Code = code;
            CreationTime = creationTime;
        }

        public EmailProof(string email) : this(0, email, new Random().Next(100000, 1000000), DateTime.Now)
        {

        }
    }
}
