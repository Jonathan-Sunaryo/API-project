using API.Context;
using API.Model;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public Tuple<int, string> Login(LoginVM loginVM)
        {
            try { 
            var checkEmail = context.Employees.Where(b => b.Email == loginVM.Email).FirstOrDefault();
            
            var checkPhone = context.Employees.Where(b => b.Phone ==loginVM.Phone).FirstOrDefault();

            if (checkEmail != null || checkPhone != null)
            {
                
                    var password = (from e in context.Set<Employee>()
                                    where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                                    join a in context.Set<Account>() on e.NIK equals a.NIK
                                    select a.Password).Single();

                    var nik = (from e in context.Set<Employee>()
                               where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                               join a in context.Set<Account>() on e.NIK equals a.NIK
                               select e.NIK).Single();

                    var checkPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, password);
              
                //Password salah
                if (checkPassword == false)
                {
                    return Tuple.Create(3, "");
                }
                //Login Berhasil
                else
                {
                    return Tuple.Create(1, nik);
                }
            }
            //Email atau Nomor telepon salah
            else
            {
                return Tuple.Create(2, "");
            }
            }
            catch
            {
                return Tuple.Create(0, "");
            }
        }

        public int Register(RegisterVM registerVM)
        {
            var result = 0;
            try
            {
                Employee employee = new Employee()
                {
                    NIK = registerVM.NIK,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Salary = registerVM.Salary,
                    Gender = (Model.Gender)registerVM.Gender,
                    Phone = registerVM.Phone,
                    BirthDate = registerVM.BirthDate,
                    Email = registerVM.Email,
                };

                var checkNIK = context.Employees.Find(employee.NIK);

                var checkEmail = context.Employees.Where(b => b.Email == registerVM.Email).FirstOrDefault();

                var checkPhone = context.Employees.Where(b => b.Phone == registerVM.Phone).FirstOrDefault();

                if (checkNIK != null)
                {
                    return 2;
                }
                else if (checkEmail != null)
                {
                    return 3;
                }
                else if (checkPhone != null)
                {
                    return 4;
                }
                context.Employees.Add(employee);
                result = context.SaveChanges();
                Account account = new Account()
                {
                    NIK = registerVM.NIK,
                    Password = registerVM.Password
                };
                string hashPassword = Hashing.Hashing.HashPassword(account.Password);
                account.Password = hashPassword;
                context.Accounts.Add(account);
                result = context.SaveChanges();

                Education education = new Education()
                {
                    Degree = registerVM.Degree,
                    UniversityId = registerVM.UniversityId,
                    Gpa = registerVM.Gpa
                };
                context.Educations.Add(education);
                result = context.SaveChanges();

                Profiling profiling = new Profiling();
                {
                    profiling.EducationId = education.EducationId;
                    profiling.NIK = registerVM.NIK;
                }
                context.Profilings.Add(profiling);
                result = context.SaveChanges();

                AccountRole accountRole = new AccountRole();
                {
                    accountRole.AccountNIK = account.NIK;
                    accountRole.RoleId = 3;
                }
                context.AccountRoles.Add(accountRole);
                result = context.SaveChanges();
            }
            catch
            {
                return result;
            }
            return result;
        }

        public IEnumerable GetRegister()
        {
            var query = (from e in context.Set<Employee>()
                         join a in context.Set<Account>() on new { NIK = e.NIK } equals new { NIK = a.NIK }
                         join p in context.Set<Profiling>() on new { NIK = a.NIK } equals new { NIK = p.NIK }
                         join ed in context.Set<Education>() on new { EdId = p.EducationId } equals new { EdId = ed.EducationId }
                         join u in context.Set<University>() on new { UId = ed.UniversityId } equals new { UId = u.UniversityId }
                         select new
                         {
                             NIK = e.NIK,
                             FullName = e.FirstName + " " + e.LastName,
                             Gender = e.Gender == 0 ? "Male" : "Female",
                             PhoneNumber = e.Phone,
                             BirthDate = e.BirthDate,
                             Salary = e.Salary,
                             Email = e.Email,
                             Degree = ed.Degree,
                             GPA = ed.Gpa,
                             UniversityName = u.Name
                         }).AsEnumerable();
            var myList = query.ToList();
            return myList;
        }

        public ICollection GetProfile(string NIK)
        {
            var query = (from e in context.Set<Employee>()
                         join a in context.Set<Account>() on new { NIK = e.NIK } equals new { NIK = a.NIK }
                         join p in context.Set<Profiling>() on new { NIK = a.NIK } equals new { NIK = p.NIK }
                         join ed in context.Set<Education>() on new { EdId = p.EducationId } equals new { EdId = ed.EducationId }
                         join u in context.Set<University>() on new { UId = ed.UniversityId } equals new { UId = u.UniversityId }
                         where e.NIK == NIK
                         select new
                         {
                             NIK = e.NIK,
                             FullName = e.FirstName + " " + e.LastName,
                             Gender = e.Gender == 0 ? "Male" : "Female",
                             PhoneNumber = e.Phone,
                             BirthDate = e.BirthDate,
                             Salary = e.Salary,
                             Email = e.Email,
                             Degree = ed.Degree,
                             GPA = ed.Gpa,
                             UniversityName = u.Name
                         });
            return query.ToList();
        }

        public IEnumerable GetUserData(string NIK)
        {
            var query = (from e in context.Set<Employee>()
                         join a in context.Set<Account>() on new { NIK = e.NIK } equals new { NIK = a.NIK }
                         join ar in context.Set<AccountRole>() on new { NIK = a.NIK } equals new { NIK = ar.AccountNIK }
                         join r in context.Set<Role>() on new { RId = ar.RoleId } equals new { RId = r.RoleId }
                         where e.NIK == NIK
                         select new
                         {
                             Employee = e,
                             AccountRole = ar,
                             Role = r 
                         }).AsEnumerable();
            return query.ToList();
        }
    }
}
