using Microsoft.EntityFrameworkCore;

namespace Heavy.Data.Repositorys
{
    public class WriteOrReadContext : DbContext, IWriteOrRead
    {

        public WriteOrReadContext(DbContextOptions options) : base(options)
        {

        }
        public static string Conn = string.Empty;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbContext GetDbContext(string conn)
        {
            Conn = conn;
            return this;
        }
    }
}
