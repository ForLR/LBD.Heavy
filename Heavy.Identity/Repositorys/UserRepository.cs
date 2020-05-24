using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heavy.Data;
using Heavy.Data.Repository;
using Heavy.Data.Repositorys;
using Heavy.Domain.Interfaces;
using Heavy.Identity.Data;
using Heavy.Identity.Model;

namespace Heavy.Repositorys
{
    public class UserRepository: Repository<ApplicationDbContext, User>
    {
        public UserRepository(IDbContextFactory dbContextFactory) :base(dbContextFactory)
        {
           
           
        }

       
    }
}
