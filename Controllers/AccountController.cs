using IsIoTWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsIoTWeb.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Required] string username, [Required] string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User appUser = await userManager.FindByNameAsync(username);
                    if (appUser != null)
                    {
                        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, password, false, false);
                        if (result.Succeeded)
                        {
                            return Redirect("/");
                        }
                    }
                    ModelState.AddModelError(nameof(username), "Invalid Email or Password");
                }
                catch (TimeoutException)
                {
                    ModelState.AddModelError("Network Timeout", "A timout was encountered when sending the login request. Server might be down or connection is unstable.");
                }
                catch(Exception)
                {
                    ModelState.AddModelError("Unexpected error", "An unexpected error occured. Please contact the administrator.");
                }
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await signInManager.SignOutAsync();
            }
            catch (TimeoutException)
            {
                ModelState.AddModelError("Network Timeout", "A timout was encountered when sending the logout request. Server might be down or connection is unstable.");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Unexpected error", "An unexpected error occured. Please contact the administrator.");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
