using API.Model;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    //[Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }


        [HttpGet]
        public async Task<JsonResult> GetRegister()
        {
            var result = await employeeRepository.GetRegister();
            return Json(result);
        }


        //[HttpPost]
        //public async Task<JsonResult> Login()
        //{
        //    var result = await employeeRepository.Login();
        //    return Json(result);
        //}

        [HttpPost("Auth/")]
        public async Task<IActionResult> Auth(LoginVM login)
        {
            var jwtToken = await employeeRepository.Auth(login);
            var token = jwtToken.Token;

            if (token == null)
            {
                return RedirectToAction("index", "login");
            }

            HttpContext.Session.SetString("JWToken", token);
            //HttpContext.Session.SetString("Name", jwtHandler.GetName(token));
            //HttpContext.Session.SetString("ProfilePicture", "assets/img/theme/user.png");

            return RedirectToAction("index", "dashboards");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}