using API.Context;
using API.Model;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class EmployeeRepositoryUNUSED : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepositoryUNUSED(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var check = context.Employees.Find(NIK);
            if (check != null)
            {
                context.Remove(check);
            }
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
            var check = context.Employees.Find(employee.NIK);
            if (check == null)
            {
                context.Employees.Add(employee);
            }
            var result = context.SaveChanges();
            return result;
        }

        public int Update(Employee employee)
        {
            var result = 0;
            try
            {
                context.Entry(employee).State = EntityState.Modified;
            }
            catch
            {
                result = context.SaveChanges();
                return result;
            }
            result = context.SaveChanges();
            return result;
        }
    }
}
