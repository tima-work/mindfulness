using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp;
using MindfulLens_DesktopApp.Containers.Visitor;
using MindfulLens_DesktopApp.Start_forms;
using MindfulLens_DesktopApp.User_controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp
{
    public partial class VisitorForm : Form
    {
        private GraphicFeatures graphicFeatures;
        private List<UserControl> tabs;
        public VisitorForm(UserManager userManager, CognitivePartManager cognitivePartManager, ContentManager contentManager, ExerciseManager exerciseManager, SourceManager sourceManager, ReportManager reportManager, User loggedUser)
        {
            InitializeComponent();
            graphicFeatures = new GraphicFeatures(this);
            VisitorHomeUserControl visitorHomeUserControl = new VisitorHomeUserControl(graphicFeatures);
            visitorHomeUserControl.homePageBase.LogOutButton.BindFunction(LogOut);
            Size size = new Size(920, 478);
            tabs = new List<UserControl>()
            {
                visitorHomeUserControl,
                new VisitorBiasesContainer(size, graphicFeatures, loggedUser, cognitivePartManager, reportManager),
                new VisitorTheoriesContainer(size, graphicFeatures, loggedUser, cognitivePartManager, reportManager),
                new VisitorSourcesContainer(size, graphicFeatures, loggedUser, sourceManager, reportManager),
                new VisitorExercisesContainer(size, graphicFeatures, loggedUser, exerciseManager, reportManager),
                new VisitorIntroductionUserControl()
            };
            graphicFeatures.MakeNavBar(tabs);
        }

        private void LogOut(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.ShowDialog();
            this.Close();
        }
    }
}
