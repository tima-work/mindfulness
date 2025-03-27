using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Recommendations_creators.User_classes
{
    public interface IUserCalculator
    {
        public Dictionary<ForumPublication, int> CalculateUsers(User user);
    }
}
