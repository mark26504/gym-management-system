using GymManagementSystemBLL.ViewModels.AnalyticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IAnalyticsServices
    {
        AnalyticsViewModel GetAnalyticsData();
    }
}
