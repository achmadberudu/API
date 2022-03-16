using API2.Models;
using API2.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API2.Controllers
{
    [Route("api2/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        //insert data phone & email unique
        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            try
            {
                var insert = employeeRepository.Insert(employee);
                if (insert == 0)
                {
                    return BadRequest("Error, Masukkan Data Terlebih Dahulu");
                }
                else if (insert == 1)
                {
                    return Ok("Data berhasil di inputkan");
                }
                else if (insert == 101)
                {
                    return BadRequest("Gagal, Email telah terpakai");
                }
                else if (insert == 102)
                {
                    return BadRequest("Gagal, Nomor Telepon telah terpakai");
                }
                return BadRequest("Data Gagal Dimasukkan");
            }
            catch
            {
                return BadRequest("ERROR DATA TIDAK DAPAT DIMASUKKAN");
            }

        }

        //show aal data
        [HttpGet]
        public ActionResult GET()
        {
            var getAll = employeeRepository.Get();
            if (getAll.Count() == 0)
            {
                return BadRequest("Data tidak ditemukan");
            }
            return Ok(getAll);
        }
        //get by id
        [HttpGet("{NIK}")]
        public ActionResult GET(string NIK)
        {
            var getAll = employeeRepository.Get(NIK);
            if (getAll == null)
            {
                return BadRequest("Data tidak ditemukan");
            }
            return Ok(getAll);
        }
        //update employee
        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            try
            {
                var update = employeeRepository.Update(employee);
                if (update != 0)
                {
                    return Ok("Data berhasil diperbaharui");
                }
                return BadRequest("Data gagal diperbaharui");
            }
            catch
            {
                return BadRequest("ERROR");
            }

        }
        //delete by id
        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            try
            {
                var deleteData = employeeRepository.Delete(NIK);
                if (deleteData != 0)
                {
                    return Ok("Data berhasil dihapus");
                }
                else
                {
                    return BadRequest($"ID Employee = {NIK} Not Found");
                }
            }
            catch (Exception)
            {
                return BadRequest($"Error Employee dengan ID = {NIK} Tidak Ditemukan");
            }

        }
    }
}
