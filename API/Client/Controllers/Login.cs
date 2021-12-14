using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
