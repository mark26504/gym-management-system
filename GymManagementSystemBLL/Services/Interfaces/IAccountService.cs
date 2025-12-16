using GymManagementSystemBLL.ViewModels.AccountViewModels;
using GymManagementSystemDAL.Entities;
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
    }
}
