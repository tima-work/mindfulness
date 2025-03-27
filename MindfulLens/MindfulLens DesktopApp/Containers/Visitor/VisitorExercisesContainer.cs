using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.User_controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Containers.Visitor
{
    public class VisitorExercisesContainer : AbstractExercisesContainer
    {
        public VisitorExercisesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, ExerciseManager exerciseManager, ReportManager reportManager)
        {
            this.Name = "Custom exercises";
            exercisesUserControl = new CustomExercisesUserControl(graphicFeatures, WatchExercise, exerciseManager);
            CustomExercisesUserControl visitorControl = (CustomExercisesUserControl)exercisesUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, exerciseManager, visitorControl.FillInExercises, reportManager);
            visitorControl.noFilterActionMenuLayout.AddNewButton.BindFunction(GoToAddNewExercise);
        }
    }
}
