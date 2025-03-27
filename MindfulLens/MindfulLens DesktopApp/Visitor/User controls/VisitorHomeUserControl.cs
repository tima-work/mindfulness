using Logic_Layer.Classes;
using Microsoft.Identity.Client.NativeInterop;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace MindfulLens_DesktopApp.User_controls
{
    public partial class VisitorHomeUserControl : UserControl
    {
        private GraphicFeatures graphicFeatures;
        public HomePageBase homePageBase { get; private set; }
        public VisitorHomeUserControl(GraphicFeatures graphicFeatures)
        {
            InitializeComponent();
            this.graphicFeatures = graphicFeatures;
            this.Name = "Home";
            homePageBase = new HomePageBase(graphicFeatures, this, "Welcome to MindfulLens! Our desktop application empowers you on a journey of self-discovery and growth. Unlock a personalized path tailored to your goals, and customize it to fit your needs. Feel free to create your own biases, theories, sources, and exercises, or explore those crafted by fellow users. New to MindfulLens? Start with our introduction page. Visit http://mindfullens.org for more features and resources. Begin your transformative journey with MindfulLens today!", new Point(140, 185));
            //CustomButton LogOutButton = new CustomButton("Log out", new Size(160, 60), new Font("Segoe UI", 15.75f), new Point(20, 398), graphicFeatures);
            //this.Controls.Add(LogOutButton);
        }
        private void LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            graphicFeatures.UnableCursor();
            this.Cursor = Cursors.Hand;
            LinkLabel.BackColor = Color.FromArgb(255, 172, 223, 225);
        }

        private void LinkLabel_MouseLeave(object sender, EventArgs e)
        {
            graphicFeatures.EnableCursor();
            this.Cursor = Cursors.Default;
            LinkLabel.BackColor = Color.Transparent;
        }
    }
}
