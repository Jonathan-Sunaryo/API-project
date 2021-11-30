using API.Base;
using API.Context;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public EmployeesController(MyContext myContext, EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
            context = myContext;
        }

        [HttpGet("Profile/{Key}")]
        public ActionResult<RegisterVM> GetProfile(string key)
        {
            var result = employeeRepository.GetProfile(key);
            if (result.Count != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil ditampilkan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }

        [HttpPost("Register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dibuat" });
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "NIK tidak boleh sama" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Email tidak boleh sama" });
            }
            else if (result == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Nomor telepon tidak boleh sama" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Data tidak berhasil dibuat" });
        }
            


        [Authorize(Roles = "Manager,Director")]
        [HttpGet("Register")]
        public ActionResult<RegisterVM> GetRegister()
        {
           
            var result = employeeRepository.GetRegister();
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil ditampilkan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }


        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS berhasil");
        }

        [Authorize]
        [HttpGet("Tes")]
        public ActionResult Tes()
        {
            return Ok("Tes JWT Employee");
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("TesManager")]
        public ActionResult TesManager()
        {
            return Ok("Tes Manager");
        }

        [HttpPost("Login")]
        public ActionResult<LoginVM> GetLogin(LoginVM loginVM)
        {
            var result = employeeRepository.Login(loginVM);
            if (result.Item1 == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email atau Nomor telepon salah" });
            }
            else if (result.Item1 == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password salah" });
            }
            else if (result.Item1 == 1)
            {
                var result2 = employeeRepository.GetProfile(result.Item2);
                if (result2 != null)
                {
                    var getUserData = (from e in context.Set<Employee>()
                                 join a in context.Set<Account>() on new { NIK = e.NIK } equals new { NIK = a.NIK }
                                 join ar in context.Set<AccountRole>() on new { NIK = a.NIK } equals new { NIK = ar.AccountNIK }
                                 join r in context.Set<Role>() on new { RId = ar.RoleId } equals new { RId = r.RoleId }
                                 where e.NIK == result.Item2
                                 select new
                                 {
                                     Employee = e,
                                     AccountRole = ar,
                                     Role = r

                                 }).ToList();

                    var claims = new List<Claim>
                    {
                          new  Claim(JwtRegisteredClaimNames.Email, getUserData[0].Employee.Email)
                    };

                    foreach (var RoleName in getUserData)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, RoleName.Role.Name));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );

                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                    employeeRepository.GetProfile(result.Item2);

                    return Ok(new { status = HttpStatusCode.OK, idtoken , result2, message = "Login Berhasil" });

                }
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });

            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }

    }
}
