using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.User_controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Containers.Admin
{
    public class AdminSourceContainer : AbstractSourcesContainer
    {
        public AdminSourceContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, SourceManager sourceManager, ReportManager reportManager)
        {
            Name = "Sources";
            sourcesUserControl = new SourcesUserControl(graphicFeatures, WatchSource, sourceManager);
            SourcesUserControl adminControl = (SourcesUserControl)sourcesUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, sourceManager, adminControl.FillInSources, reportManager);
            adminControl.fullActionMenuLayout.AddNewButton.BindFunction(GoToAddNewExercise);
        }
    }
}
