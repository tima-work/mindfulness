using Logic_Layer.Classes;

namespace MindfulLens_WebApp.Models
{
    public class UpdateUserModel : CommonUserModel
    {
        public IFormFile? formFile { get; set; }

        public UpdateUserModel() { }

        public UpdateUserModel(User user)
        {
            this.Name = user.Name;
            this.Email = user.Email;
            this.Username = user.Username;
            if (user.Photo != null)
            {
                MemoryStream stream = new MemoryStream(user.Photo);
                this.formFile = new FormFile(stream, 0, user.Photo.Length, "name", "filename");
            }
            else
                this.formFile = null;
        }
    }
}
