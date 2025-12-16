using GymManagementSystemBLL.ViewModels.BookingViewModels;
using GymManagementSystemBLL.ViewModels.MembershipsViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IBookingServices
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerandCategory();
        IEnumerable<MemberForSessionViewModel> GetAllMembersForSession(int id);
        bool CreateBooking(CreateBookingViewModel model);
        IEnumerable<MemberForSelectListViewModel> GetMemberForDropdown(int id);
        bool MemberAttended(MemberAttendOrCancelViewModel model);
        bool CancelBooking(MemberAttendOrCancelViewModel model);
    }
}