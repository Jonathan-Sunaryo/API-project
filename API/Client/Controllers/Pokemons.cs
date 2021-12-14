using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class Pokemons : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
