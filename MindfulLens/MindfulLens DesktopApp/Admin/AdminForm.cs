using MindfulLens_DesktopApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using MindfulLens_DesktopApp.User_controls;
using System.Reflection.Metadata.Ecma335;
using MindfulLens_DesktopApp.Add_and_edit.Add;
using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using MindfulLens_DesktopApp.Containers.Admin;
using MindfulLens_DesktopApp.Visitor.User_controls;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Start_forms;

namespace MindfulLens_DesktopApp
{
    public partial class AdminForm : Form
    {
        private List<UserControl> tabs;
        private GraphicFeatures graphicFeatures;
        public AdminForm(UserManager userManager, CognitivePartManager cognitivePartManager, ContentManager contentManager, ExerciseManager exerciseManager, SourceManager sourceManager, ReportManager reportManager, Logic_Layer.Classes.User loggedUser)
        {
            InitializeComponent();
            Size size = new Size(920, 478);
            graphicFeatures = new GraphicFeatures(this);
            AdminHomeUserControl adminHomeUserControl = new AdminHomeUserControl(graphicFeatures);
            adminHomeUserControl.homePageBase.LogOutButton.BindFunction(LogOut);
            tabs = new List<UserControl>()
            {
                adminHomeUserControl,
                new AdminExercisesContainer(size, graphicFeatures, loggedUser, exerciseManager, reportManager),
                new AdminBiasesContainer(size, graphicFeatures, loggedUser, cognitivePartManager, reportManager),
                new AdminTheoriesContainer(size, graphicFeatures, loggedUser, cognitivePartManager, reportManager),
                new AdminSourceContainer(size, graphicFeatures, loggedUser, sourceManager, reportManager),
                new AdminNewsContainer(size, graphicFeatures, loggedUser, contentManager),
                new AdminInterviewsContainer(size, graphicFeatures, loggedUser, contentManager),
                new AdminIntroductionUserControl()
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
