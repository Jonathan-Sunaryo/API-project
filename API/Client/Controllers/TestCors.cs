using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class TestCors : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
