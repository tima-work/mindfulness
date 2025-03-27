using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Repository_Interfaces
{
    public interface IEmailProofRepository
    {
        EmailProof? GetLastProofByEmail(string email);
        void DeleteProofsByEmail(string email);
    }
}
