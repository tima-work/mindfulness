using Logic_Layer.Classes;
using System.ComponentModel.DataAnnotations;

namespace MindfulLens_WebApp.Models
{
    public class ReviewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Forum publication length must be at least 5 characters long")]
        public string Text { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Review ranking must be between 1 and 10")]
        public int Ranking { get; set; }

        public ReviewModel() { }

        public ReviewModel(Review review)
        {
            Text = review.Text;
            Ranking = review.Ranking;
        }
    }
}
