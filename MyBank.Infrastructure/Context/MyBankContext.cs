using Microsoft.EntityFrameworkCore;
using MyBank.Domain.Entities;
using MyBank.Infrastructure.Configurations;

namespace MyBank.Infrastructure.Context
{
    public class MyBankContext : DbContext
    {
        public MyBankContext(DbContextOptions<MyBankContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(new UserConfiguration().Configure);
        }
    }
}
