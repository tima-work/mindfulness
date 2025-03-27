using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Repository_Interfaces
{
    public interface IPublishOfficialRepository
    {
        void AddPublishOfficial(PublishOfficial publishOfficial);
        void DeletePublishOfficial(PublishOfficial publishOfficial);
        void DeletePublishOfficialByPublication(TitledPublication titledPublication);
        PublishOfficial[] GetPublishOfficialByUser(User user);
        PublishOfficial[] GetAllPublishOfficial();
    }
}
