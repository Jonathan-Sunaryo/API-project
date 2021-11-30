using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        private UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            this.universityRepository = repository;
        }


        [HttpGet("GetStudents")]
        public ActionResult GetStudents()
        {
            var result = universityRepository.GetStudents();
            if (result.Key.Count!= 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data berhasil ditampilkan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, Message = "Data tidak ada" });
        }
    }
}
