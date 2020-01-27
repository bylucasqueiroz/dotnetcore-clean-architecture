using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBank.Domain.Users.Entities;

namespace MyBank.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(c => c.Id)
                .HasName("Id");

            builder.Property(c => c.Agency)
                .IsRequired()
                .HasColumnName("Agency");

            builder.Property(c => c.Account)
                .IsRequired()
                .HasColumnName("Account");

            builder.Property(c => c.Password)
                .IsRequired()
                .HasColumnName("Password");

            builder.Property(c => c.IdData)
                .IsRequired();
        }
    }
}
