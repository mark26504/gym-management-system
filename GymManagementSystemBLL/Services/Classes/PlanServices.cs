using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.PlanViewModels;
using GymManagementSystemDAL.Entities;
using GymManagementSystemDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Classes
{
    public class PlanServices : IPlanServices
    {
        #region Connection

        #region Feild

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region CTOR
        public PlanServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #endregion

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans = _unitOfWork.GetRepository<Plan>().GetAll() ?? [];

            var PLanViewModel = Plans.Select(X => new PlanViewModel
            {
                Id = X.Id,
                Name = X.Name,
                Description = X.Description,
                DurationDays = X.DurationDays,
                Price = X.Price,
                IsActive = X.IsActive,
            });

            return PLanViewModel;
        }

        public PlanViewModel GetPlanById(int id)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(id) ?? null;

            return new PlanViewModel
            {
                Id = Plan.Id,
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
                IsActive = Plan.IsActive,
            };
        }

        public UpdatePlanViewModel GetPlanToUpdate(int PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);

            if (Plan is null || Plan .IsActive == false || HasActiveMembership(PlanId)) return null;

            return new UpdatePlanViewModel
            {
                Name = Plan.Name,
                Description = Plan.Description,
                DurationDays = Plan.DurationDays,
                Price = Plan.Price,
            };
        }

        public bool UpdatePlan(int PlanId, UpdatePlanViewModel updatedPlan)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMembership(PlanId)) return false;

            try
            {
                // Tuples [C# New Feature]
                (Plan.Description, Plan.DurationDays , Plan.Price, Plan.UpdatedAt)
              = (updatedPlan.Description, updatedPlan.DurationDays, updatedPlan.Price, DateTime.Now);

                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool ToggleStatus(int PlanId)
        {
            var Plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (Plan is null || HasActiveMembership(PlanId)) return false;

            Plan.IsActive = Plan.IsActive == true ? false : true; //If Else

            try
            {
                _unitOfWork.GetRepository<Plan>().Update(Plan);
                return _unitOfWork.SaveChanges() > 0;

            }
            catch (Exception)
            {

                return false;
            }
        }

        #region Helper Methods

        private bool HasActiveMembership(int PlanId)
        {
            var ActiveMemberShip = _unitOfWork.GetRepository<Membership>()
                                              .GetAll(X => X.PlanId == PlanId && X.Status == "Active");
            return ActiveMemberShip.Any();
        }

        #endregion
    }
}
