using Data_Infrastructure.Repositories;
using Logic_Layer.Classes;
using Logic_Layer.Custom_exceptions;
using Logic_Layer.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MindfulLens_WebApp.Models;
using System.IO;
using System.Reflection;
using System.Security.Claims;

namespace MindfulLens_WebApp.Pages
{
    [Authorize(Roles = "User")]
    public class ProfileModel : PageModel
    {
        [BindProperty]
        public UpdateUserModel updateUserModel { get; set; }

        private UserRepository userRepository;
        private UserManager userManager;

        public string errorMessage { get; private set; } = "";
        public string successMessage { get; private set; } = "";

        public ProfileModel()
        {
            userRepository = new UserRepository();
            userManager = new UserManager(userRepository);
        }

        public void OnGet()
        {
            WebFunctions.CheckIfBanned(HttpContext);

            Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (claim != null)
            {
                User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                if (user != null)
                {
                    updateUserModel = new UpdateUserModel(user);
                }
            }
        }

        public IActionResult OnPostChangeProfile()
        {
            if (ModelState.IsValid)
            {
                Claim? claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
                if (claim != null)
                {
                    User? user = userManager.GetUserById(Convert.ToInt32(claim.Value));
                    if (user != null)
                    {
                        try
                        {
                            byte[]? photo;
                            if (updateUserModel.formFile != null)
                            {
                                MemoryStream memoryStream = new MemoryStream();
                                updateUserModel.formFile.CopyTo(memoryStream);
                                photo = memoryStream.ToArray();
                            }
                            else
                                photo = null;
                            User newUser = new User(user.Id, updateUserModel.Name, updateUserModel.Email, user.HashedPassword, user.Salt, updateUserModel.Username, photo, user.Banned, user.Importancy);
                            userManager.UpdateUser(newUser);
                            errorMessage = "";
                            successMessage = "Successfully updated";
                            return Page();
                        }
                        catch (WrongInputException ex)
                        {
                            successMessage = "";
                            errorMessage = ex.Message;
                            return Page();
                        }
                    }
                }
                successMessage = "";
                errorMessage = "Something went wrong. Please, try again";
            }
            return Page();
        }
    }
}
