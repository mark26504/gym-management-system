using GymManagementSystemBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystemBLL.Services.Interfaces
{
    public interface IPlanServices
    {
        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel GetPlanById(int id);

        // Get Plan Details That Will Be Updated
        UpdatePlanViewModel GetPlanToUpdate(int PlanId);

        // Apply Update
        bool UpdatePlan(int PlanId, UpdatePlanViewModel model);

        //Toggle Plan IsActive
        bool ToggleStatus(int PlanId);
    }
}
