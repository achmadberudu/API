using API2.Context;
using API2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
            public IEnumerable MasterEmployee()
            {
                var data = myContext.Employees
               
                    .Join(myContext.Accounts, e => e.NIK, a => a.NIK, (e, a) => new { e, a })
                    .Join(myContext.Profilings, ea => ea.a.NIK, p => p.NIK, (ea, p) => new { ea, p })
                    .Join(myContext.Educations, eap => eap.p.Education_id, ed => ed.Id, (eap, ed) => new { eap, ed })
                    .Join(myContext.Universities, eaped => eaped.ed.University_id, u => u.Id, (eaped, u) => new { eaped, u })
                    .Select(d => new
                    {

                    NIK = d.eaped.eap.ea.e.NIK.ToString(),
                            FullName = $"{d.eaped.eap.ea.e.FirstName} {d.eaped.eap.ea.e.LastName}",
                            Phone = d.eaped.eap.ea.e.Phone,
                            Gender = d.eaped.eap.ea.e.Gender.ToString(),                           
                            Email = d.eaped.eap.ea.e.Email,
                            BirthDate = d.eaped.eap.ea.e.BirtDate,
                            Salary = d.eaped.eap.ea.e.Salary,
                            Education_Id = d.eaped.eap.p.Education_id,
                            GPA = d.eaped.ed.GPA,
                            Degree = d.eaped.ed.Degree,
                            UniversityName = d.u.Name

                    });;
            return data;
                
            }
    }
}
   

