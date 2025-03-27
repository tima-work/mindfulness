using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Repository_Interfaces
{
    public interface IAdminRequestRepository
    {
        AdminRequest[] GetAdminRequestsByUser(User user);
        AdminRequest[] GetApplicationAdminRequests();
        void AddAdminRequest(AdminRequest adminRequest);
        void DeleteAdminRequest(AdminRequest adminRequest);
        void DeleteAdminRequestsByUser(User user);
    }
}
