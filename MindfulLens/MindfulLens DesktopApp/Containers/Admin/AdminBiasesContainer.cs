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
    public class AdminBiasesContainer : AbstractCognitivePartContainer
    {
        public AdminBiasesContainer(Size size, GraphicFeatures graphicFeatures, User loggedUser, CognitivePartManager cognitivePartManager, ReportManager reportManager)
        {
            this.Name = "Biases";
            cognitivePartsUserControl = new BiasesUserControl(graphicFeatures, WatchCognitivePart, cognitivePartManager);
            BiasesUserControl adminControl = (BiasesUserControl)cognitivePartsUserControl;
            ProtectedConstructor(size, graphicFeatures, loggedUser, cognitivePartManager, adminControl.FillInCognitiveParts, "How to overcome", "Bias", Color.Black, Color.FromArgb(255, 244, 239, 136), Color.Black, reportManager);
            adminControl.fullActionMenuLayout.AddNewButton.BindFunction(GoToAddNewCognitivePart);
        }
    }
}
