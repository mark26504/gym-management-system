using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.ViewModels.AnalyticsViewModels
{
    public class AnalyticsViewModel
    {
        public int TotalMembers { get; set; }
        public int ActiveMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int UpcomgingSessions { get; set; }
        public int OngoingSessions { get; set; }
        public int CompletedSessions { get; set; }
    }
}
