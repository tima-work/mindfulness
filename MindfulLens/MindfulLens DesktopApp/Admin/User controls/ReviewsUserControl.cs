using Logic_Layer.Classes;
using MindfulLens_DesktopApp.My_tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindfulLens_DesktopApp.User_controls
{
    public partial class ReviewsUserControl : UserControl
    {
        public ReviewsUserControl(GraphicFeatures graphicFeatures)
        {
            InitializeComponent();
            //this.Name = "Reviews";
            //User creator = new User("Bob", "bob@gmail.com", "bob12345", "bob");
            //User creator2 = new User("Pablo Diego José Francisco de Paula Juan Nepomuceno María de los Remedios Cipriano de la Santísima Trinidad Ruiz y Picasso", "picasso@gmail.com", "picasso12345", "picasso");
            //Review[] reviews = new Review[7]
            //{
            //    new Review("Hello", creator, 5),
            //    new Review("I recently stumbled upon this website while searching for resources to improve my critical thinking skills, and I must say, I'm thoroughly impressed! The content is incredibly insightful and well-structured, making it easy to navigate through various topics related to rationality. From cognitive biases to decision-making frameworks, the site covers a wide range of subjects in a clear and concise manner.\r\n\r\nOne aspect that stands out is the interactive exercises section, where users can apply the concepts they've learned in real-world scenarios. These exercises not only reinforce understanding but also encourage practical application, which is essential for mastering rational thinking.\r\n\r\nAnother highlight is the community aspect of the website. I've had the opportunity to engage in thought-provoking discussions with like-minded individuals, sharing perspectives and learning from others' experiences. It's refreshing to be part of a community that values rational discourse and critical inquiry.\r\n\r\nOverall, I highly recommend this website to anyone looking to enhance their rationality skills. Whether you're a novice seeking to understand the basics or a seasoned thinker looking for advanced insights, this site has something for everyone. Kudos to the team behind this invaluable resource!", creator, 8),
            //    new Review("Discovering this website was a game-changer for me. It's a treasure trove of practical tools and insights for honing rational thinking. The interactive exercises and vibrant community make learning engaging and rewarding. Highly recommended!", creator, 1),
            //    new Review("Incredible resource for growth!", creator, 3),
            //    new Review("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", creator, 10),
            //    new Review("Hello", creator2, 7),
            //    new Review("MindfulLens offers a rich array of tools and insights to cultivate rational thinking and creativity. Highly recommended for anyone on a journey of self-discovery.", creator2, 4),
            //};
            //GraphicReview[] graphicReviews = new GraphicReview[7];

            //for (int i = 0; i < reviews.Length; i++)
            //    graphicReviews[i] = new GraphicReview(reviews[i]);

            //WhiteLinesScroll container = new WhiteLinesScroll(new Size(this.Width - 28, 299), new Point(14, 89), graphicReviews, 10);
            ////NoFilterActionMenuLayout noFilterActionMenuLayout = new NoFilterActionMenuLayout(graphicFeatures, this, container);
        }
    }
}
