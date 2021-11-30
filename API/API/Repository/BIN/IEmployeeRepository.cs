using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(string NIK);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);
    }
}
