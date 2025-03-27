using System.ComponentModel.DataAnnotations;

namespace MindfulLens_WebApp.Models
{
    public class ForumPublicationModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Forum publication length must be at least 5 characters long")]
        public string Text { get; set; }

        public ForumPublicationModel() { }
    }
}
