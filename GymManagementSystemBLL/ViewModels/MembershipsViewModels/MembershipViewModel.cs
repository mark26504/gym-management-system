using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.MembershipsViewModels
{
    public class MembershipViewModel
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
