using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.AnalyticsViewModels;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class AnalyticsServices : IAnalyticsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public AnalyticsViewModel GetAnalyticsData()
        {
            var Sessions = _unitOfWork.GetRepository<Session>().GetAll();
            return new AnalyticsViewModel()
            {
                ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(X => X.Status == "Active").Count(),
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                UpcomgingSessions = Sessions.Count(X => X.StartDate > DateTime.Now),
                OngoingSessions = Sessions.Count(X => X.StartDate <= DateTime.Now && X.EndDate >= DateTime.Now),
                CompletedSessions = Sessions.Count(X => X.EndDate < DateTime.Now)
            };
        }
    }
}
