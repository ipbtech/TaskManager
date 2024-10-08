﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using TaskManager.DAL.Models;

namespace TaskManager.Dal.Config
{
    public class ProjectDbConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(proj => proj.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(proj => proj.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
            builder.Property(proj => proj.Description).HasColumnName("description").HasMaxLength(200);
            builder.Property(proj => proj.CreatedDate).HasColumnName("create_date").IsRequired();
            builder.Property(proj => proj.Status).HasColumnName("status").IsRequired();
            builder.Property(proj => proj.Image).HasColumnName("image");

            builder.Property(proj => proj.StartDate).HasColumnName("start_date");
            builder.Property(proj => proj.EndDate).HasColumnName("end_date");


            builder.HasOne(proj => proj.Admin).WithMany(user => user.AdminProjects)
                .HasPrincipalKey(user => user.Id).HasForeignKey(proj => proj.AdminId)
                .OnDelete(DeleteBehavior.SetNull);

            var z = builder.HasMany(proj => proj.ProjectUsers).WithMany(user => user.UserProjects);
        }
    }
}
