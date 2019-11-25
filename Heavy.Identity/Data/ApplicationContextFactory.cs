using Heavy.Identity.Data;
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
        public ApplicationDbContext CreateDbContext(string[] args)
        {
        

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseMySQL("Server=47.101.221.220;port=3306;uid=lanbudai;pwd=123258lR.;Database=Heavy");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
