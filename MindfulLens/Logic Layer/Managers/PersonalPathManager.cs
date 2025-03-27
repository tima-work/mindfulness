using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class PersonalPathManager
    {
        private IPersonalPathRepository personalPathRepository;
        private IPathStageRepository pathStageRepository;

        public PersonalPathManager(IPersonalPathRepository personalPathRepository, IPathStageRepository pathStageRepository)
        {
            this.personalPathRepository = personalPathRepository;
            this.pathStageRepository = pathStageRepository;
        }

        public void AddPersonalPath(PersonalPath personalPath)
        {
            personalPathRepository.AddPersonalPath(personalPath);
        }

        public void DeletePersonalPath(PersonalPath personalPath)
        {
            personalPathRepository.DeletePersonalPath(personalPath);
        }

        public PersonalPath? GetPersonalPathByUser(User user)
        {
            return personalPathRepository.GetPersonalPathByUser(user);
        }
    }
}
