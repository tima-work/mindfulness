using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class Bias_PageModel : PageModel
    {
        public CognitivePart? bias { get; private set; } = null;
        private CognitivePartManager manager;
        private CognitivePartRepository repository;
        private UserRepository userRepository;

        public Bias_PageModel()
        {
            userRepository = new UserRepository();
            repository = new CognitivePartRepository(userRepository);
            manager = new CognitivePartManager(repository);
        }

        public void OnGet(int? bias_id)
        {
            if (bias_id != null)
                bias = manager.GetCognitivePartById((int)bias_id);
        }
    }
}
