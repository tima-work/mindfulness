using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MindfulLens_WebApp.Pages.Admin
{
    [Authorize(Roles = "Admin, Me")]
    public class IntroductionModel : PageModel
    {
        public void OnGet()
        {
            WebFunctions.CheckIfBanned(HttpContext);
        }
    }
}
