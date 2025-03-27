using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class Exercise_PageModel : PageModel
    {
        private UserRepository userRepository;
        private ExerciseRepository repository;
        private ExerciseManager manager;
        public Exercise? exercise { get; private set; } = null;

        public Exercise_PageModel()
        {
            userRepository = new UserRepository();
            repository = new ExerciseRepository(userRepository);
            manager = new ExerciseManager(repository);
        }
        public void OnGet(int? exercise_id)
        {
            if (exercise_id != null)
                exercise = manager.GetExerciseById((int)exercise_id);
        }
    }
}
