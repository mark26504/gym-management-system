using GymManagementSystemBLL.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IAccountService
    {
        ApplicationUser? ValidateUser(LoginViewModel loginVM);

        Task<IdentityResult> RegisterAsync(CreateNewUser model);
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
        Task<bool> Delete(string userId);

    }
}
