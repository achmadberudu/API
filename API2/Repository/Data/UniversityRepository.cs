using API2.Context;
using API2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
