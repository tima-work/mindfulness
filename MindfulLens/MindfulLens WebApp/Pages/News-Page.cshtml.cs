using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class News_PageModel : PageModel
    {
        private ContentManager manager;
        private UserRepository userRepository;
        private ContentRepository repository;
        public Content? news { get; private set; } = null;

        public News_PageModel()
        {
            userRepository = new UserRepository();
            repository = new ContentRepository(userRepository);
            manager = new ContentManager(repository);
        }

        public void OnGet(int? news_id)
        {
            if (news_id != null)
                news = manager.GetContentById((int)news_id);
        }
    }
}
