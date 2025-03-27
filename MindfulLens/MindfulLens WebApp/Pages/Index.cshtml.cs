using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.User.HasClaim(c => c.Type == "id") && (HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("Me")))
                return Redirect("/Admin/Home");
            return Redirect("/Home");
        }
    }
}