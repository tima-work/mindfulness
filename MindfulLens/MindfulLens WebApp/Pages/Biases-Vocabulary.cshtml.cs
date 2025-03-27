using Data_Infrastructure.Repositories;
using Logic_Layer;
using Logic_Layer.Classes;
using Logic_Layer.Factories;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class Biases_vocabularyModel : PageModel
    {
        public string? sort_method { get; private set; } = null;
        public string? search_query { get; private set; } = null;
        public string? sort_order {  get; private set; } = null;
        private UserRepository userRepository;
        public CognitivePartRepository repository { get; private set; }
        public CognitivePartManager manager { get; private set; }
        public CognitivePart[] biases;
        private SortFactory sortFactory;
        public int load_number { get; private set; }
        private const int load_step = 3;
        public bool showMoreButton { get; private set; }

        public Biases_vocabularyModel()
        {
            userRepository = new UserRepository();
            repository = new CognitivePartRepository(userRepository);
            manager = new CognitivePartManager(repository);
            sortFactory = new AlphabetAndTimeSortFactory();
            load_number = load_step;
        }

        public void OnGet(string? search, string? sort, string? order, int? number)
        {
            sort_method = sort;
            search_query = search;
            sort_order = order;

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            Tuple<string?, string?> sort_setup = CommonData.CheckSort(sort, order, sortFactory);
            sort_method = sort_setup.Item1;
            sort_order = sort_setup.Item2;

            biases = manager.GetCognitiveParts(search_query, sort_method, sort_order, true, "Bias", load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref biases, load_number);
        }
    }
}
