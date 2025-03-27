using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class Source_PageModel : PageModel
    {
        private UserRepository userRepository;
        private SourceRepository repository;
        private SourceManager manager;
        public Source? source { get; private set; } = null;

        public Source_PageModel()
        {
            userRepository = new UserRepository();
            repository = new SourceRepository(userRepository);
            manager = new SourceManager(repository);
        }

        public void OnGet(int? source_id)
        {
            if (source_id != null)
                source = manager.GetSourceById((int)source_id);
        }
    }
}
