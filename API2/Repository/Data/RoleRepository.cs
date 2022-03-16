using API2.Context;
using API2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        public RoleRepository(MyContext myContext) : base(myContext)
        {
        }
    }
}
