using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        #region Login Action
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            

            var User = _accountService.ValidateUser(model);
            if (User == null)
            {
                ModelState.AddModelError("InvalidModel", "Invalid email or password.");
                return View(model);
            }
            //SignIn Manager
            var result = _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false).Result;

            if (result.IsNotAllowed)
                ModelState.AddModelError("InvalidModel", "You are not allowed to login.");
            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidModel", "Your account is locked out.");
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            return View(model);
            
        }
        #endregion

        #region Logout Action
        [HttpPost]
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Access Denied Action
        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}