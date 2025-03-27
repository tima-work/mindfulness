using Logic_Layer.Classes;
using Logic_Layer.Managers;
using MindfulLens_DesktopApp.Add_and_edit.Abstract_add_and_edit;
using MindfulLens_DesktopApp.Containers.Abstract;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.Add_and_edit.Edit
{
    public class ContentEdit : ContentAddAndEdit
    {
        public Content content { get; private set; } 
        public CustomButton DeleteButton { get; protected set; }
        private IContentContainer contentContainer;

        public ContentEdit(GraphicFeatures graphicFeatures, Size size, ContentManager contentManager, IContentContainer contentContainer) : base("Save", graphicFeatures, size, contentManager)
        {
            this.contentContainer = contentContainer;
        }

        public void PrepareForEdit(Content content)
        {
            this.content = content;
            NameTextBox.textBox.Text = content.Title;
            DescriptionTextBox.textBox.Text = content.Text;

            this.image = content.Image;

            //try
            //{
            //    PhotoPanel.Controls.Remove(PhotoPictureBox);
            //}
            //catch { }


            int picture_outline_width = 2;
            Color BackTextColor = Color.White;
            //PhotoPictureBox = new PictureBox()
            //{
            //    BackColor = BackTextColor,
            //    Size = new Size(PhotoPanel.Width - picture_outline_width * 2, PhotoPanel.Height - picture_outline_width * 2),
            //    Location = new Point(picture_outline_width, picture_outline_width)
            //};
            using (MemoryStream stream = new MemoryStream(image))
            {
                PhotoPictureBox.Image = new Bitmap(stream);
            }
            PhotoPanel.Controls.Add(PhotoPictureBox);

            MakeCreatedByUser(content.Creator, content.CreationDate);
        }
    }
}
