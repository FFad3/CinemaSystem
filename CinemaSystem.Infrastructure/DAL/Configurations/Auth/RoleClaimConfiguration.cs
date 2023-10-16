using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.Infrastructure.DAL.Configurations.Auth
{
    internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new EntityId(x));

            builder.HasIndex(x => x.ClaimName).IsUnique();
            builder.Property(x => x.ClaimName)
                .HasConversion(x => x.Value, x => new ClaimName(x))
                .IsRequired()
                .HasMaxLength(ClaimName.MaxLenght);

            builder.HasMany(c => c.Roles)
                .WithMany(x => x.Claims);
        }
    }
}