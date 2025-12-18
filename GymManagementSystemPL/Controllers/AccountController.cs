using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Entities;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _accountService.ValidateUser(model);
            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid email or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            if (result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "You are not allowed to login.");
            else if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your account is locked out.");
            else
                ModelState.AddModelError("InvalidLogin", "Invalid email or password."); // ✅ wrong password

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

        #region Admins

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var Users = await _accountService.GetUsersAsync();
            return View(Users);

        }

        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Register(CreateNewUser input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Please fill in all required fields.");
                return View(nameof(Register), input);
            }

            var result = await _accountService.RegisterAsync(input);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Admin was created successfully.";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(nameof(Register), input);
        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _accountService.Delete(id);
            if (isDeleted)
                TempData["SuccessMessage"] = "The admin has been deleted successfully.";
            else
                TempData["ErrorMessage"] = "Failed to delete the admin.";

            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}