using GymManagementSystemBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystemPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsServices _analyticsService;

        public HomeController(IAnalyticsServices analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public ActionResult Index()
        {
            var Data = _analyticsService.GetAnalyticsData();
            return View(Data);
        }

    }
}
