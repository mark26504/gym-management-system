using GymManagementSystemBLL.Services.Classes;
using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.BookingViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementSystemPL.Controllers
{
    [Authorize]
    public class BookingController(IBookingServices _bookingServices) : Controller
    {
        public IActionResult Index()
        {
            var sessions = _bookingServices.GetAllSessionsWithTrainerandCategory();
            return View(sessions);
        }

        public IActionResult GetMembersForUpcomingSession(int id)
        {
            var members = _bookingServices.GetAllMembersForSession(id);
            return View(members);
        }
        public IActionResult GetMembersForOngoingSession(int id)
        {
            var members = _bookingServices.GetAllMembersForSession(id);
            return View(members);
        }

        public IActionResult Create(int id)
        {
            var members = _bookingServices.GetMemberForDropdown(id);
            var mebersSelectList = new SelectList(members, "Id", "Name");

            ViewBag.Members = mebersSelectList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBookingViewModel model)
        {
            var result = _bookingServices.CreateBooking(model);

            if (result)
            {
                TempData["SuccessMessage"] = "Booking created successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Booking failed. Member must have an active membership.";
            }

            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId});
        }

        [HttpPost]
        public IActionResult Attended(MemberAttendOrCancelViewModel model)
        {
            var result = _bookingServices.MemberAttended(model);

            if (result)
                TempData["SuccessMessage"] = "Member attended successfully";
            else
                TempData["ErrorMessage"] = "Member attendance can't be marked";

            return RedirectToAction(nameof(GetMembersForOngoingSession), new { id = model.SessionId });
        }

        [HttpPost]
        public IActionResult Cancel(MemberAttendOrCancelViewModel model)
        {
            var result = _bookingServices.CancelBooking(model);

            if (result)
                TempData["SuccessMessage"] = "Booking cancelled successfully";
            else
                TempData["ErrorMessage"] = "Booking can't be cancelled";
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }

    }
}
