using API2.Context;
using API2.Models;
using API2.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API2.Repositorys
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var entity = context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }

        public int Insert(Employee employee)
        {
            var idEmp = Get().Count();
            if (Get().Count() == 0)
            {
                idEmp = 0;
            }
            employee.NIK = DateTime.Now.ToString("yyyy") + idEmp.ToString().PadLeft(3, '0');
            context.Employees.Add(employee);
            if (context.Employees.Where(e => e.Email == employee.Email).SingleOrDefault<Employee>() != null)
            {
                return 101;
            }
            else if (context.Employees.Where(p => p.Phone == employee.Phone).SingleOrDefault<Employee>() != null)
            {
                return 102;
            }
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}
