using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.Infrastructure.DAL.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new EntityId(x));

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username)
                .HasConversion(x => x.Value, x => new Username(x))
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Password)
               .HasConversion(x => x.Value, x => new Password(x))
               .IsRequired()
               .HasMaxLength(200);

            builder.Property(x => x.FirstName)
                .HasConversion(x => x.Value, x => new FirstName(x))
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.LastName)
                .HasConversion(x => x.Value, x => new LastName(x))
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email)
                .HasConversion(x => x.Value, x => new Email(x))
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}