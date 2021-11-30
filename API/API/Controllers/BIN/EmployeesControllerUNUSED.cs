using API.Context;
using API.Model;
using API.Repository;
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeesControllerUNUSED : ControllerBase
    {
        private EmployeeRepositoryUNUSED employeeRepository;
      
        public EmployeesControllerUNUSED(EmployeeRepositoryUNUSED employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            var result = employeeRepository.Insert(employee);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dibuat" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result = result, message = "Input NIK tidak boleh sama" });
        }

        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
                return employeeRepository.Get();
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var result = employeeRepository.Get(NIK);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil ditampilkan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data dengan NIK {NIK} tidak ditemukan" });
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var result = employeeRepository.Delete(NIK);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil didelete" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data dengan NIK {NIK} tidak ditemukan" });
        }

        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            var result = employeeRepository.Update(employee);
            if (result != 0)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil diupdate" });
            }
            else
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data dengan NIK {employee.NIK} tidak ditemukan" });
        }

    }
}
