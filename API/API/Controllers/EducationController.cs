using API.Base;
using API.Model;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : BaseController<Education, EducationRepository, int>
    {
        public EducationsController(EducationRepository educationRepository) : base(educationRepository)
        {

        }
    }
}
