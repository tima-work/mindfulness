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
    public class NewsAndInterviewsModel : PageModel
    {
        public string? sort_method { get; private set; } = null;
        public string? sort_order { get; private set; } = null;
        public string? search_query { get; private set; } = null;
        public string? filter_method { get; private set; } = null;
        private ContentManager manager;
        private ContentRepository repository;
        private UserRepository userRepository;
        private SortFactory sortFactory;
        public Content[] newsAndInterviews;
        public int load_number { get; private set; }
        private const int load_step = 3;
        public bool showMoreButton { get; private set; }

        public NewsAndInterviewsModel()
        {
            userRepository = new UserRepository();
            repository = new ContentRepository(userRepository);
            manager = new ContentManager(repository);
            sortFactory = new OnlyTimeSortFactory();
        }
        public void OnGet(string? filter, string? search, string sort, string? order, int? number)
        {
            sort_method = sort;
            search_query = search;
            sort_order = order;
            filter_method = filter;

            switch (filter)
            {
                case "News":
                    filter_method = "News";
                    break;
                case "Interview":
                    filter_method = "Interview";
                    break;
                default:
                    filter_method = null;
                    break;
            }

            load_number = WebFunctions.CheckLoadNumber(number, load_step);

            Tuple<string?, string?> sort_setup = CommonData.CheckSort(sort, order, sortFactory);
            sort_method = sort_setup.Item1;
            sort_order = sort_setup.Item2;

            newsAndInterviews = manager.GetContent(search_query, sort_method, sort_order, filter_method, load_number + 1);

            showMoreButton = CommonData.CheckForShowMoreButton(ref newsAndInterviews, load_number);
        }
    }
}
