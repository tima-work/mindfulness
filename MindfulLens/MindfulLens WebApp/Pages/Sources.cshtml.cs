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
    public class SourcesModel : PageModel
    {
        public string? sort_method { get; private set; } = null;
        public string? search_query { get; private set; } = null;
        public string? sort_order { get; private set; } = null;
        private UserRepository userRepository;
        private SourceRepository repository;
        private SourceManager manager;
        private SortFactory sortFactory;
        public Source[] sources;
        public int load_number { get; private set; }
        private const int load_step = 3;
        public bool showMoreButton { get; private set; }

        public SourcesModel()
        {
            userRepository = new UserRepository();
            repository = new SourceRepository(userRepository);
            manager = new SourceManager(repository);
            sortFactory = new AlphabetAndTimeSortFactory();
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

            sources = manager.GetSources(search_query, sort_method, sort_order, true, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref sources, load_number);
        }
    }
}
