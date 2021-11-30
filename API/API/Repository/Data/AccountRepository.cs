using API.Context;
using API.Model;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>

    {
        private readonly MyContext context;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public Tuple<int, string> Login(LoginVM loginVM)
        {
            var checkEmail = context.Employees.Where(b => b.Email == loginVM.Email).FirstOrDefault();

            var checkPhone = context.Employees.Where(b => b.Phone == loginVM.Phone).FirstOrDefault();


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

        public IEnumerable GetProfile(string NIK)
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
                         }).AsEnumerable();
            return query.ToList();
        }

        public int LoginUser(LoginVM loginVM)
        {
            var checkEmail = context.Employees.Where(b => b.Email == loginVM.Email).FirstOrDefault();

            var checkPhone = context.Employees.Where(b => b.Phone == loginVM.Phone).FirstOrDefault();

            if (checkEmail != null || checkPhone != null)
            {
                var password = (from e in context.Set<Employee>()
                                where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                                join a in context.Set<Account>() on e.NIK equals a.NIK
                                select a.Password).Single();
                var checkPassword = Hashing.Hashing.ValidatePassword(loginVM.Password, password);
                //Password salah
                if (checkPassword == false)
                {
                    return 3;
                }
                //Login Berhasil
                else
                {
                    return 1;
                }
            }
            //Email atau Nomor telepon salah
            else
            {
                return 2;
            }
        }

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            string NIKEmail;
            try
            {
                 NIKEmail = (from a in context.Employees
                                where a.Email == forgotPasswordVM.Email
                                join b in context.Accounts on a.NIK equals b.NIK
                                select b.NIK).Single();
            }
            catch (Exception)
            {
                
                return 2;
             
            }
                  
            var account = context.Accounts.Find(NIKEmail);
                    
                string uniqueString = Guid.NewGuid().ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append("<p> Hi! You are changing your Password <p>");
                sb.Append("<p> The new password is :  <p>");
                sb.Append($"<h1> {uniqueString} <h1>");
           
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("jonathansunaryojp@gmail.com");
                    mail.To.Add(forgotPasswordVM.Email);
                    mail.Subject = $"Forgot Password {DateTime.Now}";
                    mail.Body = sb.ToString();
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {

                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("jonathansunaryojp@gmail.com", "alteisen");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                catch (Exception)
                {
                    return 3;
                }
                account.Password = Hashing.Hashing.HashPassword(uniqueString);
                context.SaveChanges();
                return 1;
            }
            
        

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var checkEmail = context.Employees.Where(b => b.Email == changePasswordVM.Email).FirstOrDefault();
            int result;
            //Tidak ada email
            if (checkEmail == null)
            {
                return 2;
            }
            else
            {
                var account = context.Accounts.Find(checkEmail.NIK);
                if (Hashing.Hashing.ValidatePassword(changePasswordVM.OldPassword, account.Password)==false)
                {
                    return 3;
                }
                else if (changePasswordVM.NewPassword != changePasswordVM.ConfirmPassword)
                {
                    return 4;
                }
                else
                {
                    account.Password = Hashing.Hashing.HashPassword(changePasswordVM.NewPassword);
                    result = context.SaveChanges();
                    return result;
                }
            }
        }
    }
}
