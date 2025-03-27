using MindfulLens_DesktopApp.My_tools.Other_stuff;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Logic_Layer.Managers;

namespace MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit
{
    public abstract class ExercisesAddAndEdit : BaseAddAndEdit
    {
        protected Label NameLabel, DescriptionLabel;
        public CustomTextBox NameTextBox { get; protected set; }
        public CustomTextBox DescriptionTextBox { get; protected set; }
        protected ExerciseManager exerciseManager;
        protected ExercisesAddAndEdit(string forwardButtonText, GraphicFeatures graphicFeatures, Size size, ExerciseManager exerciseManager) : base(forwardButtonText, graphicFeatures, size)
        {
            this.exerciseManager = exerciseManager;

            Font font = new Font("Segoe UI", 15);
            int label_textbox_spacing = 20;
            int textbox_width = 680;
            Color outlineColor = Color.Black;
            Color BackTextColor = Color.FromArgb(255, 241, 175, 253);
            Color TextColor = Color.Black;


            NameLabel = new Label()
            {
                Text = "Name:",
                ForeColor = Color.White,
                Font = font,
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            NameTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(textbox_width, 40), new Point((this.Width - textbox_width) / 2, BackButton.Height + label_textbox_spacing * 2), font, graphicFeatures, true, true);


            //P = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(NameTextBox.Width, 125), new Point(NameLabel.Location.X, HandlingLabel.Location.Y + HandlingLabel.Height + label_textbox_spacing), font, graphicFeatures, true);



            DescriptionLabel = new Label()
            {
                Text = "Description:",
                Font = font,
                ForeColor = Color.White,
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            DescriptionTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(NameTextBox.Width, ForwardButton.Location.Y - NameTextBox.Location.Y - NameTextBox.Height - label_textbox_spacing * 2), new Point(NameTextBox.Location.X, NameTextBox.Location.Y + NameTextBox.Height + label_textbox_spacing), font, graphicFeatures, true, true);


            this.Controls.Add(NameLabel);
            this.Controls.Add(NameTextBox);
            this.Controls.Add(DescriptionLabel);
            this.Controls.Add(DescriptionTextBox);

            NameLabel.Location = new Point(NameTextBox.Location.X - NameLabel.Bounds.Width - 5, NameTextBox.Location.Y);
            DescriptionLabel.Location = new Point(DescriptionTextBox.Location.X - DescriptionLabel.Bounds.Width - 5, DescriptionTextBox.Location.Y);
        }
    }
}
