using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.Infrastructure.DAL.Configurations.Auth
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new EntityId(x));

            builder.HasIndex(x => x.RoleName).IsUnique();
            builder.Property(x => x.RoleName)
                .HasConversion(x => x.Value, x => new RoleName(x))
                .IsRequired()
                .HasMaxLength(RoleName.MaxLenght);

            builder.HasMany(c => c.Claims)
                .WithMany(x => x.Roles);
        }
    }
}