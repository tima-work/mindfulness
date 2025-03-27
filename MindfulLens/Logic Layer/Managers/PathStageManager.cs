using Logic_Layer.Classes;
using Logic_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Managers
{
    public class PathStageManager
    {
        private IPathStageRepository pathStageRepository;

        public PathStageManager(IPathStageRepository pathStageRepository)
        {
            this.pathStageRepository = pathStageRepository;
        }

        public Exercise[] GetActionsByPesonalPath(PersonalPath personalPath)
        {
            return pathStageRepository.GetActionsByPersonalPath(personalPath);
        }

        public void DeletePathStage(PathStage pathStage)
        {
            pathStageRepository.DeletePathStage(pathStage);
        }
    }
}
