﻿using API.Context;
using API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        private readonly MyContext context;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }
    }
}
