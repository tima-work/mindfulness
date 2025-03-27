using Logic_Layer.Classes;
using Logic_Layer.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class EmailProofManager
    {
        private IEmailProofRepository emailProofRepository;
        public EmailProofManager(IEmailProofRepository emailProofRepository)
        {
            this.emailProofRepository = emailProofRepository;
        }

        public void DeleteProofsByEmail(string email)
        {
            emailProofRepository.DeleteProofsByEmail(email);
        }


        public bool CheckCode(string email, int code)
        {
            EmailProof? emailProof = emailProofRepository.GetLastProofByEmail(email);
            if (emailProof != null && emailProof.Code == code && (DateTime.Now - emailProof.CreationTime).TotalMinutes <= 5)
                return true;
            return false;
        }
    }
}
