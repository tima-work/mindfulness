using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindfulLens_WebApp.Models;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel userModel { get; set; }

        public string errorMessage { get; private set; } = string.Empty;
        private UserRepository userRepository;
        private UserManager userManager;

        public RegisterModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostSignUp()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                User? user = userManager.Register(new User(userModel.Name, userModel.Email, userModel.Password, userModel.Username), userModel.Password);
                if (user == null)
                {
                    errorMessage = "Something went wrong. Please, try again";
                    return Page();
                }

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, Importancy.User.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("username", user.Username)
                };

                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));
                return Redirect("/");
            }
            catch (WrongInputException ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }
}
