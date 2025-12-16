using GymManagementSystemBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IMemberServices
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int MemberId);
        HealthViewModel? GetMemberHealthDetails(int MemberId);

        //Get MemberId to Update View
        UpdateMemberViewModel? UpdateMemberById(int MemberId);

        //Apply Update
        bool UpdateMemberDetails(int MemberId, UpdateMemberViewModel updatedMember);

        //Remove Member
        bool DeleteMember(int MemberId);
    }
}
