using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using Logic_Layer.Repository_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class AdminRequestManager
    {
        private IAdminRequestRepository adminRequestRepository;
        private IUserRepository userRepository;

        public AdminRequestManager(IAdminRequestRepository adminRequestRepository, IUserRepository userRepository)
        {
            this.adminRequestRepository = adminRequestRepository;
            this.userRepository = userRepository;
        }

        public AdminRequest[] GetAdminRequestsByUser(User user)
        {
            return adminRequestRepository.GetAdminRequestsByUser(user);
        }

        public AdminRequest[] GetApplicationAdminRequests()
        {
            return adminRequestRepository.GetApplicationAdminRequests();
        }

        public void AddAdminRequest(AdminRequest adminRequest)
        {
            if (string.IsNullOrWhiteSpace(adminRequest.Text))
                throw new Exception("You haven't entered text");
            adminRequestRepository.AddAdminRequest(adminRequest);
        }

        public void DeclineAdminRequest(AdminRequest adminRequest)
        {
            adminRequestRepository.DeleteAdminRequest(adminRequest);
        }

        public void AcceptAdminRequest(AdminRequest adminRequest)
        {
            User user = new User(adminRequest.Creator.Id, adminRequest.Creator.Name, adminRequest.Creator.Email, adminRequest.Creator.HashedPassword, adminRequest.Creator.Salt, adminRequest.Creator.Username, adminRequest.Creator.Photo, adminRequest.Creator.Banned, Importancy.Admin);
            userRepository.MakeAdmin(user);
            adminRequestRepository.DeleteAdminRequestsByUser(user);
        }
    }
}
