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
        public EventStoreContext()
        {

        }
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

            optionsBuilder.UseMySQL("Server=47.101.221.220;port=3306;uid=lanbudai;pwd=123258lR.;Database=Heavy");
           // base.OnConfiguring(optionsBuilder);
        }
    }
}
