using Logic_Layer.Managers;
using MindfulLens_DesktopApp.My_tools.Other_stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit
{
    public abstract class CognitivePartAddAndEdit : BaseAddAndEdit
    {
        protected Label NameLabel, HandlingLabel, DescriptionLabel;
        public CustomTextBox NameTextBox { get; protected set;}
        public CustomTextBox HandlingTextBox { get; protected set;}
        public CustomTextBox DescriptionTextBox { get; protected set;}
        protected CognitivePartManager cognitivePartManager;
        public CognitivePartAddAndEdit(string forwardButtonText, GraphicFeatures graphicFeatures, Size size, string handlingText, Color outlineColor, Color BackTextColor, Color TextColor, CognitivePartManager cognitivePartManager) : base(forwardButtonText, graphicFeatures, size)
        {
            //Font font = new Font("Segoe UI", 15);
            //int label_textbox_spacing = 20;
            //NameLabel = new Label()
            //{
            //    Text = "Name:",
            //    ForeColor = Color.White,
            //    Font = font,
            //    Location = new Point(padding, BackButton.Height + label_textbox_spacing),
            //    AutoSize = true,
            //    BorderStyle = BorderStyle.None
            //};


            //NameTextBox = new CustomTextBox(Color.FromArgb(255, 244, 239, 136), Color.Black, "", new Size(200, 80), new Point(NameLabel.Location.X, NameLabel.Location.Y + NameLabel.Height + label_textbox_spacing), font, graphicFeatures, true);


            //HandlingLabel = new Label()
            //{
            //    Text = handlingText,
            //    Font = font,
            //    ForeColor = Color.White,
            //    Location = new Point(NameTextBox.Location.X + NameTextBox.Width + 50, NameLabel.Location.Y),
            //    AutoSize = true,
            //    BorderStyle = BorderStyle.None
            //};


            //HandlingTextBox = new CustomTextBox(Color.FromArgb(255, 244, 239, 136), Color.Black, "", new Size(this.Width - padding * 2 - 50 - NameTextBox.Width, NameTextBox.Height), new Point(HandlingLabel.Location.X, NameTextBox.Location.Y), font, graphicFeatures, true);



            //DescriptionLabel = new Label()
            //{
            //    Text = "Description:",
            //    Font = font,
            //    ForeColor = Color.White,
            //    Location = new Point(NameLabel.Location.X, NameTextBox.Location.Y + NameTextBox.Height + 20),
            //    AutoSize = true,
            //    BorderStyle = BorderStyle.None
            //};


            //DescriptionTextBox = new CustomTextBox(Color.FromArgb(255, 244, 239, 136), Color.Black, "", new Size(this.Width - padding * 2 - 50 - ForwardButton.Width, 200), new Point(DescriptionLabel.Location.X, DescriptionLabel.Location.Y + DescriptionLabel.Height + label_textbox_spacing), font, graphicFeatures, true);


            //this.Controls.Add(NameLabel);
            //this.Controls.Add(NameTextBox);
            //this.Controls.Add(HandlingLabel);
            //this.Controls.Add(HandlingTextBox);
            //this.Controls.Add(DescriptionLabel);
            //this.Controls.Add(DescriptionTextBox);


















            this.cognitivePartManager = cognitivePartManager;

            Font font = new Font("Segoe UI", 15);
            int label_textbox_spacing = 20;
            NameLabel = new Label()
            {
                Text = "Name:",
                ForeColor = Color.White,
                Font = font,
                Location = new Point(padding, BackButton.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            NameTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(250, 80), new Point(NameLabel.Location.X, NameLabel.Location.Y + NameLabel.Height + label_textbox_spacing), font, graphicFeatures, true);
            NameTextBox.textBox.ScrollBars = ScrollBars.Vertical;

            HandlingLabel = new Label()
            {
                Text = handlingText,
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameLabel.Location.X, NameTextBox.Location.Y + NameTextBox.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            HandlingTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(NameTextBox.Width, 125), new Point(NameLabel.Location.X, HandlingLabel.Location.Y + HandlingLabel.Height + label_textbox_spacing), font, graphicFeatures, true);
            HandlingTextBox.textBox.ScrollBars = ScrollBars.Vertical;


            DescriptionLabel = new Label()
            {
                Text = "Description:",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameTextBox.Width + NameTextBox.Location.X + 50, NameLabel.Location.Y),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            DescriptionTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(this.Width - padding * 2 - 50 - NameTextBox.Width, HandlingTextBox.Height + HandlingTextBox.Location.Y - NameTextBox.Location.Y), new Point(DescriptionLabel.Location.X, NameTextBox.Location.Y), font, graphicFeatures, true);
            DescriptionTextBox.textBox.ScrollBars = ScrollBars.Vertical;


            this.Controls.Add(NameLabel);
            this.Controls.Add(NameTextBox);
            this.Controls.Add(HandlingLabel);
            this.Controls.Add(HandlingTextBox);
            this.Controls.Add(DescriptionLabel);
            this.Controls.Add(DescriptionTextBox);

        }
    }
}
