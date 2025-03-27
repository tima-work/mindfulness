using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.Classes;

namespace Logic_Layer.Interfaces
{
    public interface IPathStageRepository
    {
        //PathStage[] GetPathStagesByUser(User user);
        Exercise[] GetActionsByPersonalPath(PersonalPath personalPath);
        //void DeletePathStagesByUser(User user);
        void DeletePathStagesByPersonalPath(PersonalPath personalPath);
        void DeletePathStagesByAction(Exercise action);
        void AddPathStage(PathStage pathStage, PersonalPath personalPath);
        void DeletePathStage(PathStage pathStage);
        PathStage? GetNextPathStage(PersonalPath personalPath);
        PathStage? GetLastPathStage(PersonalPath personalPath);

    }
}
