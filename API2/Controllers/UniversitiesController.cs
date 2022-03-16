using API2.Base;
using API2.Models;
using API2.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [Route("api2/[controller]")]
    [ApiController]
    public class UniversitiesController : BaseController<University, UniversityRepository, int>
    {
        public UniversitiesController(UniversityRepository universityRepository) : base(universityRepository)
        {
        }
    }
}
