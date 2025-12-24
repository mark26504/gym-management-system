
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanServices _planServices;

        public PlanController(IPlanServices planServices)
        {
            _planServices = planServices;
        }

        #region Get All Plans
        public IActionResult Index()
        {
            var plans = _planServices.GetAllPlans();
            return View(plans);
        }

        #endregion

        #region Get plan Details
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "Invalid Plan ID.";
                return RedirectToAction("Index");
            }

            var plan = _planServices.GetPlanById(id);
            if (plan is null)
            {
                TempData["Error"] = "Plan not found.";
                return RedirectToAction("Index");
            }

            return View(plan);
        }
        #endregion

        #region Edit Plan

        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "Invalid Plan ID.";
                return RedirectToAction("Index");
            }
            var plan = _planServices.GetPlanToUpdate(id);
            if (plan is null)
            {
                TempData["Error"] = "Plan not found.";
                return RedirectToAction("Index");
            }
            return View(plan);
        }

        [HttpPost]
        public ActionResult Edit([FromRoute]int id, UpdatePlanViewModel updatedPlan)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Please correct the errors in the form.");
                return View(updatedPlan);
            }
            bool Result = _planServices.UpdatePlan(id, updatedPlan);
            if (!Result)
            {
                TempData["ErrorMessage"] = "Failed to update the plan. Please try again.";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction("Index");
        }
        #endregion

        #region Active & Deactive - Soft Delete
        [HttpPost]
        public ActionResult Activate(int id)
        {
            var Result = _planServices.ToggleStatus(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed Successfuly.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Change Plan Status. Please try again.";
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}