using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class Interview_PageModel : PageModel
    {
        private ContentRepository repository;
        private UserRepository userRepository;
        private ContentManager manager;
        public Content? interview { get; private set; } = null;

        public Interview_PageModel()
        {
            userRepository = new UserRepository();
            repository = new ContentRepository(userRepository);
            manager = new ContentManager(repository);
        }

        public void OnGet(int? interview_id)
        {
            if (interview_id != null)
                interview = manager.GetContentById((int)interview_id);
        }
    }
}
