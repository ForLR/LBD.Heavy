using Heavy.Identity.Data;
using LBD.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heavy.Data.Context
{

    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private IConfiguration configuration;
        public ApplicationContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public ApplicationDbContext CreateDbContext(string[] args)
        {
        

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            return new ApplicationDbContext(builder.Options);
        }
    }
}
