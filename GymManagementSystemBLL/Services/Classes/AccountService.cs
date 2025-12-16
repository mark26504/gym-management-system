using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUser? ValidateUser(LoginViewModel loginVM)
        {
            var user = _userManager.FindByEmailAsync(loginVM.Email).Result;
            if (user is null) return null;

            var isPasswordValid = _userManager.CheckPasswordAsync(user, loginVM.Password).Result;
            return isPasswordValid ? user : null;
        }
    }
}
