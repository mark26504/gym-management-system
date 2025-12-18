using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser? ValidateUser(LoginViewModel loginVM)
        {
            var user = _userManager.FindByEmailAsync(loginVM.Email).Result;
            if (user is null) return null;

            var isPasswordValid = _userManager.CheckPasswordAsync(user, loginVM.Password).Result;
            return isPasswordValid ? user : null;
        }

        public async Task<IdentityResult> RegisterAsync(CreateNewUser model)
        {
            ApplicationUser user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {


                var roleResult = await _userManager.AddToRoleAsync(user, model.Role.ToString());
                if (!roleResult.Succeeded)
                {
                    var errors = roleResult.Errors.ToArray();
                    return IdentityResult.Failed(errors);
                }
            }

            return result;
        }
        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var superAdminUsers = await _userManager.GetUsersInRoleAsync("SuperAdmin");
            if ((adminUsers == null || !adminUsers.Any()) && (superAdminUsers == null || !superAdminUsers.Any()))
                return [];
            bool isAdminUserExist = false;
            IEnumerable<UserViewModel> AdminModel = Enumerable.Empty<UserViewModel>();
            if (adminUsers is not null)
            {
                AdminModel = adminUsers.Select(x => new UserViewModel
                {
                    UserId = x.Id,
                    FullName = $"{x.FirstName.Trim()} {x.LastName.Trim()}",
                    Email = x.Email,
                    Role = "Admin"
                });

                if (superAdminUsers is null)
                    return AdminModel;
                isAdminUserExist = true;
            }
            var superAdminModel = superAdminUsers.Select(x => new UserViewModel
            {
                FullName = $"{x.FirstName.Trim()} {x.LastName.Trim()}",
                UserId = x.Id,
                Email = x.Email,
                Role = "SuperAdmin"
            });

            if (isAdminUserExist)
            {
                return AdminModel.Concat(superAdminModel);
            }

            return superAdminModel;


        }

        public async Task<bool> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;
            var rolesForUser = await _userManager.GetRolesAsync(user);
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    if (rolesForUser is not null && rolesForUser.Any())
                    {
                        await _userManager.RemoveFromRolesAsync(user, rolesForUser);
                    }
                    var result = await _userManager.DeleteAsync(user);
                    await transaction.CommitAsync();
                    return result.Succeeded;
                }
            }
            catch
            {
                return false;
            }

        }

    }
}
