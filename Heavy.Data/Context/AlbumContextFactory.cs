using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heavy.Data.Context
{

    public class AlbumContextFactory : IDesignTimeDbContextFactory<HeavyContext>
    {
        public HeavyContext CreateDbContext(string[] args)
        {
           // IConfigurationRoot configuration = new ConfigurationBuilder()
           //.SetBasePath(Directory.GetCurrentDirectory())
           //.AddJsonFile("appsettings.json")
           //.Build();
           // var connectionStr = configuration["ConnectionStrings"];

            var builder = new DbContextOptionsBuilder<HeavyContext>();
            builder.UseMySQL("Server=47.101.221.220;port=3306;uid=lanbudai;pwd=123258lR.;Database=Heavy");
            return new HeavyContext(builder.Options);
        }
    }
}
