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
    public class VisitorSourcesContainer : AbstractSourcesContainer
    {
        public VisitorSourcesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, SourceManager sourceManager, ReportManager reportManager)
        {
            Name = "Custom sources";
            sourcesUserControl = new CustomSourcesUserControl(graphicFeatures, WatchSource, sourceManager);
            CustomSourcesUserControl adminControl = (CustomSourcesUserControl)sourcesUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, sourceManager, adminControl.FillInSources, reportManager);
            adminControl.noFilterActionMenuLayout.AddNewButton.BindFunction(GoToAddNewExercise);
        }
    }
}
