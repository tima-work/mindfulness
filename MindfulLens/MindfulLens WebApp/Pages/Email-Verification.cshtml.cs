using Logic_Layer.Classes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class Email_VerificationModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            User user = new User("Bob", "bob@email.com", "bob12345", "bob");
            User user2 = new User(3, user.Name, user.Email, user.HashedPassword, user.Salt, user.Username, user.Photo, user.Banned, user.Importancy);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, Importancy.User.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("id", user2.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("username", user.Username)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));
            return Redirect("/");
        }
    }
}
