using API.Base;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private AccountRoleRepository accountRoleRepository;
       
        public AccountRolesController(AccountRoleRepository accountRoleRepository) : base(accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
          
        }

        [Authorize(Roles = "Director")]
        [HttpPost("AssignManager")]
        public ActionResult<EmailVM> AssignManager(EmailVM emailVM)
        {
            var result = accountRoleRepository.AssignManager(emailVM);
            if (result ==1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Berhasil menambah role manager" });
            }
            else 
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
        }
        }
}
