using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.User_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Containers.Admin
{
    public class AdminExercisesContainer : AbstractExercisesContainer
    {
        public AdminExercisesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, ExerciseManager exerciseManager, ReportManager reportManager)
        {
            Name = "Exercises";
            exercisesUserControl = new AdminExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
            AdminExercisesUserControl adminControl = (AdminExercisesUserControl)exercisesUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, exerciseManager, (exercisesUserControl as AdminExercisesUserControl).FillInExercises, reportManager);
            adminControl.fullActionMenuLayout.AddNewButton.BindFunction(GoToAddNewExercise);
        }
    }
}
