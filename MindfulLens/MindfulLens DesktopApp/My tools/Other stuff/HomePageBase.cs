using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class HomePageBase
    {
        private GraphicFeatures graphicFeatures;
        private UserControl userControl;
        private Label link_label;
        public CustomButton LogOutButton { get; private set; }
        public HomePageBase(GraphicFeatures graphicFeatures, UserControl userControl, string text, Point link_label_location)
        {
            this.graphicFeatures = graphicFeatures;
            this.userControl = userControl;


            Label text_label = new Label();
            text_label.Text = text;
            text_label.AutoSize = false;
            text_label.Size = new Size(700, 300);
            text_label.TextAlign = ContentAlignment.TopCenter;
            text_label.Location = new Point(110, 35);
            text_label.ForeColor = Color.White;
            text_label.Font = new Font("Segoe UI", 15.75f);


            link_label = new Label();
            link_label.Text = "http://mindfullens.org";
            link_label.Font = new Font("Segoe UI", 15.75f);
            link_label.ForeColor = Color.FromArgb(255, 68, 68, 255);
            link_label.AutoSize = true;
            link_label.Location = link_label_location;
            link_label.MouseEnter += LinkLabel_MouseEnter;
            link_label.MouseLeave += LinkLabel_MouseLeave;
            link_label.Click += LinkLabel_Click;

            LogOutButton = new CustomButton("Log out", new Size(160, 60), new Font("Segoe UI", 15.75f), new Point(20, 398), graphicFeatures);


            userControl.Controls.Add(text_label);
            userControl.Controls.Add(link_label);
            link_label.BringToFront();
            userControl.Controls.Add(LogOutButton);
        }

        private void LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            graphicFeatures.UnableCursor();
            userControl.Cursor = Cursors.Hand;
            link_label.BackColor = Color.FromArgb(255, 172, 223, 225);
        }

        private void LinkLabel_MouseLeave(object sender, EventArgs e)
        {
            graphicFeatures.EnableCursor();
            userControl.Cursor = Cursors.Default;
            link_label.BackColor = Color.Transparent;
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
    }
}
