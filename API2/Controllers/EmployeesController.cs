using API2.Base;
using API2.Models;
using API2.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API2.Controllers
{
    [Route("api2/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
        }

        [HttpGet("mEmployee")]
        public ActionResult MasterData()
        {
            var result = employeeRepository.MasterEmployee();
            return Ok(result);
        }
        [Authorize(Roles = "Manager")]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }
        [HttpGet("TestCors")]
        public ActionResult TestCors()
        {
            var result = employeeRepository.Get();
            return Ok(result);
        }
    }
}
