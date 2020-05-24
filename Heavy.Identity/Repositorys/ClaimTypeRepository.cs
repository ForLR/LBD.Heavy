﻿using Heavy.Data.Repository;
using Heavy.Data.Repositorys;
using Heavy.Identity.Data;
using Heavy.Identity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Repositorys
{
    public class ClaimTypeRepository: Repository<ApplicationDbContext, ClaimType>
    {
        public ClaimTypeRepository(IDbContextFactory dbContextFactory) : base(dbContextFactory)
        {

        }

      
    }
}
