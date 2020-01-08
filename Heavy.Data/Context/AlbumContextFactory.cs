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

    public class AlbumContextFactory : IDesignTimeDbContextFactory<HeavyContext>
    {
        private IConfiguration configuration;
        public AlbumContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public HeavyContext CreateDbContext(string[] args)
        {
           // IConfigurationRoot configuration = new ConfigurationBuilder()
           //.SetBasePath(Directory.GetCurrentDirectory())
           //.AddJsonFile("appsettings.json")
           //.Build();
           // var connectionStr = configuration["ConnectionStrings"];

            var builder = new DbContextOptionsBuilder<HeavyContext>();
            builder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            return new HeavyContext(builder.Options);
        }
    }
}
