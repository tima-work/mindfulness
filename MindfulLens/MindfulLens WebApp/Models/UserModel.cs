using System.ComponentModel.DataAnnotations;

namespace MindfulLens_WebApp.Models
{
    public class UserModel : CommonUserModel
    {

        [Required]
        [Length(8, 100, ErrorMessage = "Password length must be between 8 and 100 characters")]
        public string Password { get; set; }

        public UserModel() { }
    }
}
