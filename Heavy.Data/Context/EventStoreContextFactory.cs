using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Heavy.Data.Context
{

    public class EventStoreContextFactory : IDesignTimeDbContextFactory<EventStoreContext>
    {
        private IConfiguration configuration;
        public EventStoreContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        
        }
        public EventStoreContext CreateDbContext(string[] args)
        {
           // IConfigurationRoot configuration = new ConfigurationBuilder()
           //.SetBasePath(Directory.GetCurrentDirectory())
           //.AddJsonFile("appsettings.json")
           //.Build();
           // var connectionStr = configuration["ConnectionStrings"];

            var builder = new DbContextOptionsBuilder<EventStoreContext>();
            builder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            return new EventStoreContext(builder.Options);
        }
    }
}
