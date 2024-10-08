﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.DAL.Models;

namespace TaskManager.Dal.Config
{
    public class UserDbConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(u => u.FirstName).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasColumnName("surname").HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            builder.Property(u => u.Password).HasColumnName("password").HasMaxLength(100).IsRequired();
            builder.Property(u => u.HashPassword).HasColumnName("hash_password").IsRequired();
            builder.Property(u => u.Phone).HasColumnName("phone").HasMaxLength(15);
            builder.Property(u => u.Role).HasColumnName("role").IsRequired();
            builder.Property(u => u.RegistrDate).HasColumnName("registr_date").IsRequired();
            builder.Property(u => u.LastLoginDate).HasColumnName("lastlogin_date").IsRequired();

            builder.HasData(new User()
            {
                Id = 1,
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@admin.com",
                Password = "qwerty",
                HashPassword = "qwerty",
                Role = UserRole.SystemOwner
            });
        }
    }
}
