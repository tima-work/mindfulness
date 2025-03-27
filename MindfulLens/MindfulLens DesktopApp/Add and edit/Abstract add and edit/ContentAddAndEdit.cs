using Logic_Layer.Managers;
using MindfulLens_DesktopApp.My_tools;
using MindfulLens_DesktopApp.My_tools.Other_stuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit
{
    public abstract class ContentAddAndEdit : BaseAddAndEdit
    {
        protected Label NameLabel, PhotoLabel, DescriptionLabel;
        public CustomTextBox NameTextBox { get; protected set; }
        public CustomTextBox DescriptionTextBox { get; protected set; }
        protected Panel PhotoPanel;
        public PictureBox PhotoPictureBox { get; protected set; }
        protected CustomButton NewPhotoButton;
        protected ContentManager contentManager;
        protected OpenFileDialog photoFileDialog;
        public byte[]? image { get; protected set; } = null;
        protected ContentAddAndEdit(string forwardButtonText, GraphicFeatures graphicFeatures, Size size, ContentManager contentManager) : base(forwardButtonText, graphicFeatures, size)
        {
            this.contentManager = contentManager;
            Font font = new Font("Segoe UI", 15);
            int label_textbox_spacing = 20;
            int picture_outline_width = 2;
            Color outlineColor = Color.Black;
            Color BackTextColor = Color.White;
            Color TextColor = Color.Black;


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

            
            PhotoLabel = new Label()
            {
                Text = "Photo:",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameLabel.Location.X, NameTextBox.Location.Y + NameTextBox.Height + label_textbox_spacing),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            PhotoPanel = new Panel()
            {
                BackColor = outlineColor,
                Size = new Size(110, 110),
                Location = new Point(PhotoLabel.Location.X, PhotoLabel.Location.Y + PhotoLabel.Height + label_textbox_spacing),
            };


            PhotoPictureBox = new PictureBox()
            {
                BackColor = BackTextColor,
                Size = new Size(PhotoPanel.Width - picture_outline_width * 2, PhotoPanel.Height - picture_outline_width * 2),
                Location = new Point(picture_outline_width, picture_outline_width),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            photoFileDialog = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg;*.jpeg;*.png;)|*.jpg;*.jpeg;*.png;"
            };


            NewPhotoButton = new CustomButton("Upload new photo", new Size(220, 60), new Font("Segoe UI", 16), new Point(NameLabel.Location.X, PhotoPanel.Location.Y + PhotoPanel.Height + label_textbox_spacing), graphicFeatures);
            NewPhotoButton.BindFunction(ChoosePhoto);


            //P = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(NameTextBox.Width, 125), new Point(NameLabel.Location.X, HandlingLabel.Location.Y + HandlingLabel.Height + label_textbox_spacing), font, graphicFeatures, true);



            DescriptionLabel = new Label()
            {
                Text = "Description:",
                Font = font,
                ForeColor = Color.White,
                Location = new Point(NameTextBox.Width + NameTextBox.Location.X + 50, NameLabel.Location.Y),
                AutoSize = true,
                BorderStyle = BorderStyle.None
            };


            DescriptionTextBox = new CustomTextBox(outlineColor, BackTextColor, TextColor, "", new Size(this.Width - padding * 2 - 50 - NameTextBox.Width, PhotoPanel.Height + PhotoPanel.Location.Y - NameTextBox.Location.Y), new Point(DescriptionLabel.Location.X, NameTextBox.Location.Y), font, graphicFeatures, true);
            DescriptionTextBox.textBox.ScrollBars = ScrollBars.Vertical;


            PhotoPanel.Controls.Add(PhotoPictureBox);


            this.Controls.Add(NameLabel);
            this.Controls.Add(NameTextBox);
            this.Controls.Add(PhotoLabel);
            this.Controls.Add(PhotoPanel);
            this.Controls.Add(NewPhotoButton);
            this.Controls.Add(DescriptionLabel);
            this.Controls.Add(DescriptionTextBox);
        }

        private void ChoosePhoto(object sender, EventArgs e)
        {
            if (photoFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.image = File.ReadAllBytes(photoFileDialog.FileName);
                using (MemoryStream stream = new MemoryStream(image))
                {
                    PhotoPictureBox.Image = new Bitmap(stream);
                }
            }
        } 
    }
}
