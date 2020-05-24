using Heavy.Data.Repositorys;
using Heavy.Identity.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heavy.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>, IWriteOrRead
    {

        public DbSet<ClaimType> ClaimTypes { get; set; }

        public DbSet<User> Userss { get; set; }
        public IConfiguration configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptionsMonitor<DBConnectionOption> optionsMonitor) : base(options)
        {
            this.Conn = optionsMonitor.CurrentValue.WriteConnection;
        }
        private string Conn = string.Empty;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySql(Conn);
            //}
            optionsBuilder.UseMySql(Conn);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //这段比较重要 不然会产生user1Id外键
            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }

        public DbContext GetDbContext(string conn)
        {
            this.Conn = conn;
            return this;
        }
    }
}
