using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data.Context
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config= new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configution = config.Build();
            var connectionStr = configution["ConnectionStrings"];
            optionsBuilder.UseMySql(connectionStr);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
