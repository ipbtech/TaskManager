﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Dal;

#nullable disable

namespace TaskManager.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240615105223_ChangePasswordDefUser")]
    partial class ChangePasswordDefUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManager.Dto.Desk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("DeskColumns")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("desk_columns");

                    b.Property<int>("DeskOwnerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit")
                        .HasColumnName("private");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DeskOwnerId");

                    b.HasIndex("ProjectId");

                    b.ToTable("desks", (string)null);
                });

            modelBuilder.Entity("TaskManager.Dto.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("TaskManager.Dto.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("AttachmentsData")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("attachments");

                    b.Property<string>("ColumnOfDesk")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("column");

                    b.Property<int?>("ContractorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_date");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<int?>("DeskId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("DeskId");

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("TaskManager.Dto.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("avatar");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("lastlogin_date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("surname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.Property<DateTime>("RegistrDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("registr_date");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@admin.com",
                            FirstName = "admin",
                            LastLoginDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "admin",
                            Password = "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5",
                            RegistrDate = new DateTime(2024, 6, 15, 13, 52, 23, 208, DateTimeKind.Local).AddTicks(1450),
                            Role = 3
                        });
                });

            modelBuilder.Entity("TaskManager.Dto.UserProjectLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("users_projects", (string)null);
                });

            modelBuilder.Entity("TaskManager.Dto.Desk", b =>
                {
                    b.HasOne("TaskManager.Dto.User", "DeskOwner")
                        .WithMany("Desks")
                        .HasForeignKey("DeskOwnerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Dto.Project", "Project")
                        .WithMany("Desks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeskOwner");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskManager.Dto.Project", b =>
                {
                    b.HasOne("TaskManager.Dto.User", "Admin")
                        .WithMany("AdminProjects")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("TaskManager.Dto.Task", b =>
                {
                    b.HasOne("TaskManager.Dto.User", "Contractor")
                        .WithMany("AssigningTasks")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TaskManager.Dto.User", "Creator")
                        .WithMany("CreatingTasks")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("TaskManager.Dto.Desk", "Desk")
                        .WithMany("Tasks")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Contractor");

                    b.Navigation("Creator");

                    b.Navigation("Desk");
                });

            modelBuilder.Entity("TaskManager.Dto.UserProjectLink", b =>
                {
                    b.HasOne("TaskManager.Dto.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Dto.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManager.Dto.Desk", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManager.Dto.Project", b =>
                {
                    b.Navigation("Desks");

                    b.Navigation("ProjectUsers");
                });

            modelBuilder.Entity("TaskManager.Dto.User", b =>
                {
                    b.Navigation("AdminProjects");

                    b.Navigation("AssigningTasks");

                    b.Navigation("CreatingTasks");

                    b.Navigation("Desks");

                    b.Navigation("UserProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
