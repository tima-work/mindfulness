using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MindfulLens_WebApp.Pages
{
    public class ExercisesModel : PageModel
    {
        //private TrialExerciseRepository repository;
        private UserRepository userRepository;
        private ExerciseRepository repository;
        private ExerciseManager manager;
        private SortFactory sortFactory;
        public string? sort_method { get; private set; } = null;
        public string? search_query { get; private set; } = null;
        public string? sort_order { get; private set; } = null;
        public Exercise[] exercises;
        public int load_number { get; private set; }
        private const int load_step = 3;
        //public string? load_way { get; private set; }
        public bool showMoreButton { get; private set; }
        public ExercisesModel()
        {
            userRepository = new UserRepository();
            this.repository = new ExerciseRepository(userRepository);
            this.manager = new ExerciseManager(repository);
            this.sortFactory = new AlphabetAndTimeSortFactory();
        }

        public void OnGet(string? search, string? sort, string? order, int? number)
        {
            search_query = search;

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            Tuple<string?, string?> sort_setup = CommonData.CheckSort(sort, order, sortFactory);
            sort_method = sort_setup.Item1;
            sort_order = sort_setup.Item2;

            exercises = manager.GetExercises(search_query, sort_method, sort_order, true, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref exercises, load_number);
        }
    }
}
