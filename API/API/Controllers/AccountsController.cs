using API.Base;
using API.Model;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private AccountRepository accountRepository;
      
        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            
        }

        [HttpPost("Login")]
        public ActionResult<LoginVM> Login(LoginVM loginVM)
        {
            var result = accountRepository.Login(loginVM);
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
                var result2 = accountRepository.GetProfile(result.Item2);
                if (result2 != null)
                {
                    return Ok(new { status = HttpStatusCode.OK, result = result2, message = "Login Berhasil" });
                }
                return NotFound(new { status = HttpStatusCode.NotFound, result = result2, message = $"Data tidak ada" });

            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }


        [HttpPut("Change")]
        public ActionResult<ChangePasswordVM> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = accountRepository.ChangePassword(changePasswordVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Password berhasil diubah" });
            }
            else if (result == 2)
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            else if (result == 3)
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email ditemukan,Password lama salah" });
            else if (result == 4)
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password baru dan confirm password tidak sama" });
            else
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password tidak berhasil diubah" });
        }

        [HttpPost("Forgot")]
        public ActionResult<LoginVM> GetForgot(ForgotPasswordVM forgotPasswordVM)
        {
            var result = accountRepository.ForgotPassword(forgotPasswordVM);
            if (result==1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Password berhasil diubah" });
            }
            else if (result ==2)
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            else if (result ==3)
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Terjadi kesalahan saat pengiriman Email" });
            else
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password tidak berhasil diubah" });
        }

    }
}
