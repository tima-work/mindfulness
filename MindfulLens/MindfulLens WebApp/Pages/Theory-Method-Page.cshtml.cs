using Logic_Layer.Classes;
using Logic_Layer.Managers;
using Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data_Infrastructure.Repositories;

namespace MindfulLens_WebApp.Pages
{
    public class Theory_Method_PageModel : PageModel
    {
        public CognitivePart? theory { get; private set; } = null;
        private CognitivePartManager manager;
        private CognitivePartRepository repository;
        private UserRepository userRepository;

        public Theory_Method_PageModel()
        {
            userRepository = new UserRepository();
            repository = new CognitivePartRepository(userRepository);
            manager = new CognitivePartManager(repository);
        }

        public void OnGet(int? theory_id)
        {
            if (theory_id != null)
                theory = manager.GetCognitivePartById((int)theory_id);

        }
    }
}
