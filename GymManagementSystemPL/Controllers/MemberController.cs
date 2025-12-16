using GymManagementSystemBLL.Services.Interfaces;
using GymManagementSystemBLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberServices _memberServices;

        public MemberController(IMemberServices memberServices)
        {
            _memberServices = memberServices;
        }

        #region GetAllMembers
        public ActionResult Index()
        {
            var members = _memberServices.GetAllMembers();
            return View(members);
        }
        #endregion

        #region Get Member Details

        public ActionResult MemberDetails(int id) {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction("Index");
            }

            var memberDetails = _memberServices.GetMemberDetails(id);
            if (memberDetails == null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction("Index");
            }

            return View(memberDetails);
        }

        #endregion

        #region Get Health Record
        public ActionResult HealthRecordDetails(int id) 
        { 
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction("Index");
            }

            var healthRecord = _memberServices.GetMemberHealthDetails(id);
            if (healthRecord == null)
            {
                TempData["ErrorMessage"] = "Health record not found.";
                return RedirectToAction("Index");
            }

            return View(healthRecord);
        }
        #endregion

        #region Create Member
        public ActionResult Create() 
        {
            return View();
        }

        public ActionResult CreateMember(CreateMemberViewModel CreatedMember) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(nameof(Create), CreateMember);
            }

            bool Result = _memberServices.CreateMember(CreatedMember);
            if (Result)
                TempData["SuccessMessage"] = "Member Created Successfully.";
            else 
                TempData["ErrorMessage"] = "Faild To Create Member!";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Edite Member
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction("Index");
            }

            var member = _memberServices.UpdateMemberById(id);
            if (member == null)
            {
                TempData["ErrorMessage"] = "Health record not found.";
                return RedirectToAction("Index");
            }

            return View(member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute]int id, UpdateMemberViewModel updatedMember) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Fields");
                return View(updatedMember);
            }
            var Result = _memberServices.UpdateMemberDetails(id, updatedMember);
            if (Result)
                TempData["SuccessMessage"] = "Member Updated Successfully.";
            else
                TempData["ErrorMessage"] = "Faild To Update Member!";

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Delete Member
        public ActionResult Delete(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction(nameof(Index));
            }
            var Member = _memberServices.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }

        public ActionResult ConfirmedDelete([FromForm]int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member ID.";
                return RedirectToAction("Index");
            }
            var Result = _memberServices.DeleteMember(id);
            if (Result)
                TempData["SuccessMessage"] = "Member Deleted Successfully.";
            else
                TempData["ErrorMessage"] = "Faild To Delete Member!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}