using API2.Controllers;
using API2.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Base
{
    [Route("api2/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;   

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        //insert data phone & email unique
        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            try
            {
                var insert = repository.Insert(entity);
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
        [HttpGet]
        public ActionResult<Entity> Get()
        {
           
                var getAll = repository.Get();
                if (getAll.Count() == 0)
                {
                    return BadRequest("Data tidak ditemukan");
                }
                return Ok(getAll);

        }
        [HttpGet("{id}")]
        public ActionResult GET(Key id)
        {

            var getAll = repository.Get(id);
            if (getAll == null)
            {
                return BadRequest("Data tidak ditemukan");
            }
            return Ok(repository.Get(id));
        }
        //update employee
        [HttpPut]
        public ActionResult Update(Entity entity, Key id)
        {
            try
            {
                var update = repository.Update(entity, id);
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
        [HttpDelete("{id}")]
        public ActionResult Delete(Key id)
        {
            try
            {
                var deleteData = repository.Delete(id);
                if (deleteData != 0)
                {
                    return Ok("Data berhasil dihapus");
                }
                else
                {
                    return BadRequest($"ID Employee = {id} Not Found");
                }
            }
            catch (Exception)
            {
                return BadRequest($"Error Employee dengan ID = {id} Tidak Ditemukan");
            }

        }
       
    }
}
