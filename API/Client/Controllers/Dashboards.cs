using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize(Roles = "Manager,Director")]
    public class Dashboards : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
