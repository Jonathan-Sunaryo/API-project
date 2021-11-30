using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<MyContext, Profiling, string>
    {
        private readonly MyContext context;
        public ProfilingRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
