using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class HomeModel : PageModel
    {
        public bool showButtons { get; private set; }
        public static string CurrentPage = "/";
        public void OnGet()
        {
            CurrentPage = "Home";
            if (HttpContext.User.HasClaim(c => c.Type == "id"))
                showButtons = false;
            else
                showButtons = true;

        }
    }
}
