using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.MembershipsViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystemPL.Controllers
{
    [Authorize]
    public class MembershipController(IMembershipServices _membershipServices) : Controller
    {
        public IActionResult Index()
        {
            var memberships = _membershipServices.GetAllMemberships();
            return View(memberships);
        }

        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateMembershipViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _membershipServices.CreateMembership(model);
                if (result)
                {
                    TempData["SuccessMessage"] = "Membership created successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to create membership!";
                }
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Membership cannot be created, Check your data!";
            LoadDropdowns();
            return View(model);
        }

        public IActionResult Cancel(int id)
        {
            var result = _membershipServices.DeleteMembership(id);
            if (result)
            {
                TempData["SuccessMessage"] = "Membership cancelled successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete membership!";
            }
            return RedirectToAction("Index");
        }

        #region Helper Methods
        public void LoadDropdowns()
        {
            var members = _membershipServices.GetMembersForDropdown();
            var plans = _membershipServices.GetPlansForDropdown();

            ViewBag.Members = new SelectList(members, "Id", "Name");
            ViewBag.Plans = new SelectList(plans, "Id", "Name");
        }
        #endregion
    }
}
