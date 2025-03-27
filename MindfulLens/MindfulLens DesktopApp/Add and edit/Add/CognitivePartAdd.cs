using Logic_Layer.Classes;
using Logic_Layer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Add
{
    public class CognitivePartAdd : CognitivePartAddAndEdit
    {
        public CognitivePartAdd(GraphicFeatures graphicFeatures, Size size, string handlingText, User creator, Color outlineColor, Color BackTextColor, Color TextColor, CognitivePartManager cognitivePartManager, string classname) : base("Add", graphicFeatures, size, handlingText, outlineColor, BackTextColor, TextColor, cognitivePartManager)
        {

            MakeCreatedByUser(creator, DateTime.Now);

        //    DateTime dateTime = DateTime.Now;
        //    CreatedByLabel = new Label()
        //    {
        //        Text = $"Created by @{creator.Username}  on {dateTime.ToString("dd/MM/yyyy")}",
        //        ForeColor = Color.White,
        //        Font = new Font("Segoe UI", 15),
        //        Location = new Point(0, 0),
        //        AutoSize = true,
        //        BorderStyle = BorderStyle.None
        //    };

        //    this.Controls.Add(CreatedByLabel);

        //    CreatedByLabel.Location = new Point(this.Width - padding - CreatedByLabel.Bounds.Width, padding);

        //    FakeCreatedByLabel = new Label()
        //    {
        //        Text = $"Created by ",
        //        ForeColor = Color.White,
        //        Font = new Font("Segoe UI", 15),
        //        Location = new Point(CreatedByLabel.Location.X, padding),
        //        AutoSize = true,
        //        BorderStyle = BorderStyle.None,
        //        BackColor = Color.Green,
        //    };


        //    this.Controls.Add(FakeCreatedByLabel);

        //    UserLabel = new Label()
        //    {
        //        Text = $"@{creator.Username}",
        //        ForeColor = Color.White,
        //        BackColor = Color.FromArgb(255, 96, 200, 252),
        //        Font = new Font("Segoe UI", 15),
        //        Location = new Point(FakeCreatedByLabel.Location.X + FakeCreatedByLabel.Bounds.Width, padding),
        //        AutoSize = true,
        //        BorderStyle = BorderStyle.None
        //    };


        //    this.Controls.Remove(FakeCreatedByLabel);
        //    this.Controls.Add(UserLabel);
        //    UserLabel.Size = new Size(UserLabel.Bounds.Width, UserLabel.Bounds.Height);
        //    UserLabel.Location = new Point(UserLabel.Location.X - 10, UserLabel.Location.Y);
        //    FakeCreatedByLabel.BringToFront();
        //    UserLabel.BringToFront();
        }

        public void PrepareForAdd()
        {
            NameTextBox.textBox.Text = string.Empty;
            DescriptionTextBox.textBox.Text = string.Empty;
            HandlingTextBox.textBox.Text = string.Empty;
        }


    }
}
