using Logic_Layer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MindfulLens_DesktopApp.My_tools
{
    public class GraphicReview : GraphicContent
    {
        public GraphicReview(Review review) : base(review)
        {
            CircularPictureBox ProfilePictureBox = new CircularPictureBox();
            int picture_width = Convert.ToInt32(this.Width * 0.1) - horizontal_spacing * 2;
            ProfilePictureBox.Size = new Size(Convert.ToInt32(picture_width * 0.7), Convert.ToInt32(picture_width * 0.7 * 1.25));
            ProfilePictureBox.Location = new Point(Convert.ToInt32((this.Width * 0.1 - ProfilePictureBox.Width) / 2), 0);
            ProfilePictureBox.BackColor = Color.White;


            //CircularPictureBox forePictureBox = new CircularPictureBox()
            //{
            //    Size = new Size(backPictureBox.Width - 6, backPictureBox.Height - 6),
            //    Location = new Point(backPictureBox.Location.X + 2, backPictureBox.Location.Y + 2),
            //    BackColor = Color.White
            //};


            Label authorNameLabel = new Label()
            {
                Text = review.Creator.Name,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 15),
                Location = new Point(0, ProfilePictureBox.Height),
                MaximumSize = new Size(picture_width, 0),
                BorderStyle = BorderStyle.None,
                BackColor = Color.Transparent
            };


            ContentLabel.MaximumSize = new Size(Convert.ToInt32(this.Width * 0.8), 0);
            ContentLabel.Text = review.Text;


            RightLabel.Text = $"{review.Ranking}/10";
            RightLabel.MaximumSize = new Size(this.Width / 10, 0);
            //RightLabel.Cursor

            this.Controls.Add(ProfilePictureBox);
            //this.Controls.Add(forePictureBox);
            this.Controls.Add(authorNameLabel);
            this.Controls.Add(ContentLabel);
            this.Controls.Add(RightLabel);


            int whole_height = ContentLabel.Bounds.Height;
            if (ProfilePictureBox.Height + authorNameLabel.Bounds.Height + horizontal_spacing > whole_height)
                whole_height = ProfilePictureBox.Height + authorNameLabel.Bounds.Height + horizontal_spacing;


            RightLabel.Height = RightLabel.Bounds.Height;
            authorNameLabel.Height = authorNameLabel.Bounds.Height;
            RightLabel.Height = RightLabel.Bounds.Height;


            this.Height = whole_height;
            ProfilePictureBox.Location = new Point(Convert.ToInt32((this.Width * 0.1 - ProfilePictureBox.Width) / 2), 0);
            //forePictureBox.Location = new Point(backPictureBox.Location.X + 3, backPictureBox.Location.Y + 3);
            //forePictureBox.BringToFront();
            authorNameLabel.Location = new Point(Convert.ToInt32((this.Width * 0.1 - authorNameLabel.Bounds.Width) / 2), ProfilePictureBox.Location.Y + ProfilePictureBox.Height + horizontal_spacing);


            ContentLabel.Location = new Point(Convert.ToInt32(this.Width * 0.1), (whole_height - ContentLabel.Bounds.Height) / 2);


            RightLabel.Location = new Point(Convert.ToInt32(this.Width * 0.9 + (this.Width * 0.1 - RightLabel.Bounds.Width) / 2), (whole_height - RightLabel.Bounds.Height) / 2);
        }
    }
}
