using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.User_controls
{
    public partial class AdminHomeUserControl : UserControl
    {
        private GraphicFeatures graphicFeatures;
        public HomePageBase homePageBase { get; private set; }
        public AdminHomeUserControl(GraphicFeatures graphicFeatures)
        {
            InitializeComponent();

            this.Name = "Home";

            this.graphicFeatures = graphicFeatures;

            homePageBase = new HomePageBase(graphicFeatures, this, "Welcome to the MindfulLens Desktop Admin Application. Explore a range of functionalities tailored for content creation and management. For additional features, visit our website at                                      . New to our platform? Go to introduction tab for a comprehensive overview of how everything works here. We hope you find our application intuitive and enjoyable to use", new Point(492, 95));

            //AdminHomeLabel.Text = "Welcome to MindfulLens desktop application for admins. Here you can find some functionalities, mostly related to creating content. You can also visit http://mindfullens.org for more features. If you are new, you should watch introduction to understand, how everything works in  here. Hope you will enjoy using our application!";
        }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        private void LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            graphicFeatures.UnableCursor();
            this.Cursor = Cursors.Hand;
            //LinkLabel.BackColor = Color.FromArgb(255, 172, 223, 225);
        }

        private void LinkLabel_MouseLeave(object sender, EventArgs e)
        {
            graphicFeatures.EnableCursor();
            //LinkLabel.BackColor = Color.Transparent;
        }

        private void LogOut(object sender, EventArgs e)
        {
            
        }
    }
}
