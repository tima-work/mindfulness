using Logic_Layer;
using Logic_Layer.Classes;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit
{
    public abstract class BaseAddAndEdit : UserControl
    {
        public CustomButton BackButton { get; protected set; }
        public CustomButton ForwardButton { get; protected set; }
        protected int padding = 14;
        protected Label CreatedByLabel, FakeCreatedByLabel, UserLabel;
        protected GraphicFeatures graphicFeatures;
        public BaseAddAndEdit(string forwardButtonText, GraphicFeatures graphicFeatures, Size size)
        {
            this.Size = size;
            this.BackColor = Color.FromArgb(255, 102, 153, 155);


            this.graphicFeatures = graphicFeatures;


            BackButton = new CustomButton("← Back", new Size(160, 60), new Font("Segoe UI", 16), new Point(padding, padding), graphicFeatures);
            ForwardButton = new CustomButton(forwardButtonText, new Size(160, 60), new Font("Segoe UI", 16), new Point(size.Width - 160 - padding, size.Height - 60 - padding), graphicFeatures);


            this.Controls.Add(BackButton);
            this.Controls.Add(ForwardButton);
        }

        public void MakeCreatedByUser(User creator, DateTime creationDate)
        {
            CreatedByLabel = new Label()
            {
                Text = $"Created by @{creator.Username}  on {creationDate.ToString("dd/MM/yyyy")}",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15),
                Location = new Point(0, 0),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };

            this.Controls.Add(CreatedByLabel);

            CreatedByLabel.Location = new Point(this.Width - padding - CreatedByLabel.Bounds.Width, padding);

            FakeCreatedByLabel = new Label()
            {
                Text = $"Created by ",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15),
                Location = new Point(CreatedByLabel.Location.X, padding),
                AutoSize = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.Green,
            };


            this.Controls.Add(FakeCreatedByLabel);

            UserLabel = new Label()
            {
                ForeColor = Color.White,
                BackColor = Color.FromArgb(255, 96, 200, 252),
                Font = new Font("Segoe UI", 15),
                Location = new Point(FakeCreatedByLabel.Location.X + FakeCreatedByLabel.Bounds.Width, padding),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };
            if (creator.Banned)
                UserLabel.Text = $"@{CommonData.userBanned}";
            else
                UserLabel.Text = $"@{creator.Username}";


            this.Controls.Remove(FakeCreatedByLabel);
            this.Controls.Add(UserLabel);
            UserLabel.Size = new Size(UserLabel.Bounds.Width, UserLabel.Bounds.Height);
            UserLabel.Location = new Point(UserLabel.Location.X - 10, UserLabel.Location.Y);
            FakeCreatedByLabel.BringToFront();
            UserLabel.BringToFront();
        }

        public void ChangeCreatedByUserColor(Color color)
        {
            CreatedByLabel.ForeColor = color;
        }
    }
}
