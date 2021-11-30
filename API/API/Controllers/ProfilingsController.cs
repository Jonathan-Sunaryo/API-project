using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilingsController : BaseController<Profiling, ProfilingRepository, string>
    {
        public ProfilingsController(ProfilingRepository profilingRepository) : base(profilingRepository)
        {

        }
    }
}
