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
    public class VisitorTheoriesContainer : AbstractCognitivePartContainer
    {
        public VisitorTheoriesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, CognitivePartManager cognitivePartManager, ReportManager reportManager)
        {
            this.Name = "Custom theories";
            cognitivePartsUserControl = new CustomTheoriesUserControl(graphicFeatures, WatchCognitivePart, cognitivePartManager);
            CustomTheoriesUserControl visitorControl = (CustomTheoriesUserControl)cognitivePartsUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, cognitivePartManager, visitorControl.FillInCognitiveParts, "How to develop: ", "Theory", Color.White, Color.FromArgb(255, 39, 70, 144), Color.White, reportManager);
            visitorControl.noFilterActionMenuLayout.AddNewButton.BindFunction(GoToAddNewCognitivePart);
        }
    }
}
