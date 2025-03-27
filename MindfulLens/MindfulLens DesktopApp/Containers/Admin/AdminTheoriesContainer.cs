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
    public class AdminTheoriesContainer : AbstractCognitivePartContainer
    {
        public AdminTheoriesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, CognitivePartManager cognitivePartManager, ReportManager reportManager)
        {
            this.Name = "Theories and methods";
            cognitivePartsUserControl = new TheoriesAndMethodsUserControl(graphicFeatures, WatchCognitivePart, cognitivePartManager);
            TheoriesAndMethodsUserControl adminControl = (TheoriesAndMethodsUserControl)cognitivePartsUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, cognitivePartManager, adminControl.FillInCognitiveParts, "How to develop", "Theory", Color.White, Color.FromArgb(255, 39, 70, 144), Color.White, reportManager);
            adminControl.fullActionMenuLayout.AddNewButton.BindFunction(GoToAddNewCognitivePart);
        }
    }
}
