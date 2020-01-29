using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Users.Entities;
using MyBank.Infrastructure.Configurations;

namespace MyBank.Infrastructure.Context
{
    public class MyBankContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MyBank;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserConfiguration().Configure);
        }
    }
}
