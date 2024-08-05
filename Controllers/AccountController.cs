using INFT3050.Models;
using INFT3050.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace INFT3050.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AuthUser> _signInManager;
        private readonly UserManager<AuthUser> _userManager;
        public AccountController(SignInManager<AuthUser> signInManager, UserManager<AuthUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            UserVM userVM = new UserVM
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email
            };
               return View(userVM);
        }
        public IActionResult Login()
        {   
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username!);
                    if (user != null && await _userManager.IsInRoleAsync(user, "Customer"))
                    {
                        // If RememberMe is selected, create a persistent cookie
                        if (model.RememberMe)
                        {
                            var authProperties = new AuthenticationProperties
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30) // Set the expiration as needed
                            };

                            await _signInManager.SignInAsync(user, authProperties);
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                        }

                        return RedirectToAction("Index", "Home");
                    }

                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "Only customers can sign in.");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }
            return View(model);
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                AuthUser user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.NewPassword!);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

                    if (roleResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Delete the user if role assignment fails
                        await _userManager.DeleteAsync(user);

                        foreach (var error in roleResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
