using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace MindfulLens_WebApp
{
    public class WebFunctions
    {
        public static void CheckIfBanned(HttpContext httpContext)
        {
            UserRepository userRepository = new UserRepository();
            UserManager userManager = new UserManager(userRepository);
            Claim? claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (claim != null)
            {
                User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                if (user != null && user.Banned)
                {
                    httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    httpContext.Response.Redirect("/Banned");
                }
            }
        }

        public static int CheckLoadNumber(int? number, int load_step)
        {
            if (number == null || number < 0)
                return load_step;
            return (int)number + load_step;
        }

        public static void ShortenPageURL(string? search, string? sort, string? order, int? number, string page_name, HttpContext httpContext)
        {
            string search_var = "";
            string sort_var = "";
            string order_var = "";
            string number_var = "";
            if (search != null)
                search_var = $"search={search}&";
            if (sort != null)
                sort_var = $"sort={sort}&";
            if (order != null)
                order_var = $"order={order}&";
            if (number != null)
                number_var = $"number={number}";
            httpContext.Response.Redirect($"/{page_name}?{search_var}{sort_var}{order_var}{number_var}");
        }
    }
}
