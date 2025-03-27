using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private UserRepository userRepository;
        private UserManager userManager;
        public string errorMessage { get; private set; } = "";

        public LoginModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {
            string? email = Request.Form["EmailInput"];
            string? password = Request.Form["PasswordInput"];
            if (email != null && password != null)
            {
                try
                {
                    User? user = userManager.Login(email, password);
                    if (user != null)
                    {
                        if (user.Banned)
                            return Redirect("/Banned");

                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Role, user.Importancy.ToString()),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim("id", user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("username", user.Username)
                        };

                        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));
                        return Redirect("/");
                    }
                    errorMessage = "Something was incorrect";
                }
                catch (WrongInputException ex)
                {
                    errorMessage = ex.Message;
                }
            }
            else
            {
                errorMessage = "You haven't filled in all the required fields";
            }

            return Page();

        }
    }
}
