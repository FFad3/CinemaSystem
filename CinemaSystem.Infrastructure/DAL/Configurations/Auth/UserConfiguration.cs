﻿using CinemaSystem.Core.Entities;
using CinemaSystem.Core.ValueObjects.Auth;
using CinemaSystem.Core.ValueObjects.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaSystem.Infrastructure.DAL.Configurations.Auth
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
                .HasMaxLength(Username.MaxLenght);

            builder.Property(x => x.Password)
               .HasConversion(x => x.Value, x => new Password(x))
               .IsRequired()
               .HasMaxLength(Password.MaxLenght);

            builder.Property(x => x.FirstName)
                .HasConversion(x => x.Value, x => new FirstName(x))
                .IsRequired()
                .HasMaxLength(FirstName.MaxLenght);

            builder.Property(x => x.LastName)
                .HasConversion(x => x.Value, x => new LastName(x))
                .IsRequired()
                .HasMaxLength(LastName.MaxLenght);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email)
                .HasConversion(x => x.Value, x => new Email(x))
                .IsRequired()
                .HasMaxLength(Email.MaxLenght);

            builder.Property(x=>x.RoleId)
                .HasConversion(x => x.Value, x => new EntityId(x));

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);
        }
    }
}