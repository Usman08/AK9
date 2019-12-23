using AK9.AppHelper.Models;
using AK9.AppHelper.Utils;
using AK9.DAL.DataSeeding;
using AK9.DAL.EntityModel.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace AK9.Admin.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly AspNetUserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AccountController(
            AspNetUserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IOptions<AppSettings> appSettings,
            ILogger<AccountController> logger
            ) : base(appSettings, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            IdentityDataInitializer.SeedData(_userManager, _roleManager);
        }

        public IActionResult SignIn()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                _logger.LogDebug("User already logged in, so redirecting to dashboard.", User.GetUserId());
                return RedirectToAction(actionName: "Index", controllerName: "Dashboard");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _logger.LogDebug("Logging in user.", model.Username);
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogDebug("User logged in successfully, now redirecting to dashboard. ", model.Username);
                return RedirectToAction("Index", "Dashboard");
            }

            _logger.LogDebug("User logged in failed", model.Username);
            ModelState.AddModelError(string.Empty, "Login Failed");
            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            _logger.LogDebug("Logging out user.", User.GetUserId());
            await _signInManager.SignOutAsync();
            _logger.LogDebug("Logged out user.", User.GetUserId());
            return RedirectToAction(actionName: "SignIn", controllerName: "Account");
        }
    }
}