using API.Context;
using API.Model;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public int AssignManager(EmailVM emailVM)
        {
            int result;
            try
            {
                var NIKEmail = (from a in context.Employees
                                where a.Email == emailVM.Email
                                join b in context.Accounts on a.NIK equals b.NIK
                                select b.NIK).Single();

                AccountRole accountRole = new AccountRole();
                {
                    accountRole.AccountNIK = NIKEmail;
                    accountRole.RoleId = 2;
                }
                context.AccountRoles.Add(accountRole);
                return result = context.SaveChanges();
            }
            catch
            {
                return 0;
            }
        }
    }
}
