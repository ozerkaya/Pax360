using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pax360.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Module_Order") == null || _httpContextAccessor.HttpContext.Session.GetString("Module_Order").ToString() == "False")
            {
                return RedirectToAction("Logoff", "Account");
            }
            return View();
        }
    }
}
