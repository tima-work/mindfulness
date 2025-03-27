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
    public class VisitorBiasesContainer : AbstractCognitivePartContainer
    {
        public VisitorBiasesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, CognitivePartManager cognitivePartManager, ReportManager reportManager)
        {
            this.Name = "Custom biases";
            cognitivePartsUserControl = new CustomBiasesUserControl(graphicFeatures, WatchCognitivePart, cognitivePartManager);
            CustomBiasesUserControl visitorControl = (CustomBiasesUserControl)cognitivePartsUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, cognitivePartManager, visitorControl.FillInCognitiveParts, "How to overcome: ", "Bias", Color.Black, Color.FromArgb(255, 244, 239, 136), Color.Black, reportManager);
            visitorControl.noFilterActionMenuLayout.AddNewButton.BindFunction(GoToAddNewCognitivePart);
        }
    }
}
