using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IPersonalPathRepository
    {
        PersonalPath? GetPersonalPathByUser(User user);
        void AddPersonalPath(PersonalPath personalPath);
        void DeletePersonalPath(PersonalPath personalPath);
        void UpdatePersonalPath(PersonalPath personalPath);
    }
}
