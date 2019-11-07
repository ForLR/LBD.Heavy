using Heavy.Data.Mappings;
using Heavy.Domain.Core.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Data.Context
{
    public class EventStoreContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvents { get; set; }
        public EventStoreContext(DbContextOptions<EventStoreContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var config= new ConfigurationBuilder().AddJsonFile("appsettings.json");
            //var configution = config.Build();
            //var connectionStr = configution["ConnectionStrings"];
            //optionsBuilder.UseMySql(connectionStr);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
