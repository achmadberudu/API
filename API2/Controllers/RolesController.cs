using API2.Base;
using API2.Models;
using API2.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role,RoleRepository , int>
    {
        public RolesController(RoleRepository roleRepository) : base(roleRepository)
        {
        }

        //[HttpGet("TestJWT")]
        //public ActionResult TestJWT()
        //{
        //    return Ok("Test JWT Berhasil");
        //}
    }
    
}
