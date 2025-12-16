using GymManagementSystemBLL.ViewModels.MembershipsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IMembershipServices
    {
        IEnumerable<MembershipViewModel> GetAllMemberships();
        IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown();
        IEnumerable<PlanForSelectListViewModel> GetPlansForDropdown();
        bool CreateMembership(CreateMembershipViewModel model);
        bool DeleteMembership(int memberId);
    }
}
