using System.ComponentModel.DataAnnotations;

namespace MindfulLens_WebApp.Models
{
    public abstract class CommonUserModel
    {
        [Required]
        [EmailAddress]
        [Length(8, 100, ErrorMessage = "Email length must be between 8 and 100 characters")]
        public string Email { get; set; }

        [Required]
        [Length(5, 100, ErrorMessage = "Username length must be between 5 and 100 characters")]
        [RegularExpression(@"[A-Za-z0-9]+", ErrorMessage = "Username can only include letters and numbers")]
        public string Username { get; set; }

        [Required]
        [Length(1, 100, ErrorMessage = "Name length must be between 1 and 100 characters")]
        public string Name { get; set; }
    }
}
