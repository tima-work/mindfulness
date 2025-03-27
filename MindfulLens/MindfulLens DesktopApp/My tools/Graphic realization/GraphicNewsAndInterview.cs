using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class GraphicNewsAndInterview : GraphicContent
    {
        private GraphicFeatures graphicFeatures;
        public GraphicNewsAndInterview(Content content, GraphicFeatures graphicFeatures, EventHandler contentFunc) : base(content)
        {
            //int horizontal_padding = 5;
            //int vertical_padding = 10;
            this.graphicFeatures = graphicFeatures;


            PictureBox IconPictureBox = new PictureBox();
            int picture_width = Convert.ToInt32(this.Width * 0.15) - horizontal_spacing * 2;
            IconPictureBox.Size = new Size(picture_width, picture_width);
            IconPictureBox.Location = new Point(Convert.ToInt32((this.Width * 0.15 - IconPictureBox.Width) / 2), 0);
            IconPictureBox.BackColor = Color.White;
            IconPictureBox.BackgroundImageLayout = ImageLayout.Zoom;
            using (MemoryStream stream = new MemoryStream(content.Image))
            {
                IconPictureBox.BackgroundImage = new Bitmap(stream);
            }


            //CircularPictureBox forePictureBox = new CircularPictureBox()
            //{
            //    Size = new Size(backPictureBox.Width - 6, backPictureBox.Height - 6),
            //    Location = new Point(backPictureBox.Location.X + 2, backPictureBox.Location.Y + 2),
            //    BackColor = Color.White
            //};





            ContentLabel.Text = content.Title;
            ContentLabel.MaximumSize = new Size(Convert.ToInt32(this.Width * 0.7), 0);


            RightLabel.Text = "Read more";
            RightLabel.MaximumSize = new Size(Convert.ToInt32(this.Width * 0.15), 0);
            //RightLabel.Cursor


            this.Controls.Add(IconPictureBox);
            //this.Controls.Add(forePictureBox);
            this.Controls.Add(ContentLabel);
            this.Controls.Add(RightLabel);


            int whole_height = ContentLabel.Bounds.Height;
            if (IconPictureBox.Height > whole_height)
                whole_height = IconPictureBox.Height;


            RightLabel.Height = RightLabel.Bounds.Height;
            RightLabel.MouseEnter += ReadMore_Hover;
            RightLabel.MouseLeave += ReadMore_Unhover;
            RightLabel.Click += contentFunc;


            this.Height = whole_height;
            IconPictureBox.Location = new Point(Convert.ToInt32((this.Width * 0.15 - IconPictureBox.Width) / 2), (whole_height - IconPictureBox.Height) / 2);
            //forePictureBox.Location = new Point(backPictureBox.Location.X + 3, backPictureBox.Location.Y + 3);
            //forePictureBox.BringToFront();


            ContentLabel.Location = new Point(Convert.ToInt32(this.Width * 0.15), (whole_height - ContentLabel.Bounds.Height) / 2);


            RightLabel.Location = new Point(Convert.ToInt32(this.Width * 0.85 + (this.Width * 0.15 - RightLabel.Bounds.Width) / 2), (whole_height - RightLabel.Bounds.Height) / 2);
        }

        private void ReadMore_Hover(object sender, EventArgs e)
        {
            RightLabel.BackColor = Color.FromArgb(60, 255, 255, 255);
            graphicFeatures.UnableCursor();
            Cursor = Cursors.Hand;
        }

        private void ReadMore_Unhover(object sender, EventArgs e)
        {
            RightLabel.BackColor = Color.Transparent;
            graphicFeatures.EnableCursor();
            Cursor = Cursors.Default;
        }
    }
}
