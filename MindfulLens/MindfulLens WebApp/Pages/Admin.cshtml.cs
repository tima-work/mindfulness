using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages
{
    public class AdminModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Redirect("/Admin/Home");
        }
    }
}
