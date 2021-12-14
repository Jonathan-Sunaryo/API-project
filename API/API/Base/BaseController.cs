using API.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class BaseController<Entity,Repository,Key> : ControllerBase
    where Entity : class
    where Repository : IRepository<Entity, Key>

    {
        private readonly Repository repository;
        public BaseController (Repository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var result = repository.Insert(entity);
            if (result != 0)
            {
                return Ok(result);
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Data tidak berhasil dibuat" });
        }

        [HttpGet]
        public ActionResult <Entity> Get()
        {
            var result = repository.Get();
            if (result.Count()!= 0)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ada" });
        }

        [HttpGet("{Key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data dengan Id {key} tidak ditemukan" });
        }
        
        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil didelete" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data dengan Id {key} tidak ditemukan" });
        }

        [HttpPut("{Key}")]
        public ActionResult Update(Entity entity,Key key)
        {
            var result = repository.Update(entity,key);
            if (result != 0)
            {
                return Ok(result);
            }
            else
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak berhasil diupdate" });
        }

    }
}
