﻿// <auto-generated />
using System;
using DotnetBoilerplate.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DotnetBoilerplate.Domain.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DotnetBoilerplate.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable("roles_role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(467),
                            Name = "Admin",
                            UpdatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(468)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(469),
                            Name = "Member",
                            UpdatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 701, DateTimeKind.Utc).AddTicks(470)
                        });
                });

            modelBuilder.Entity("DotnetBoilerplate.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_joined");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsStaff")
                        .HasColumnType("boolean")
                        .HasColumnName("is_staff");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnType("boolean")
                        .HasColumnName("is_superuser");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users_user", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5856),
                            DateJoined = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5856),
                            Email = "admin@email.com",
                            IsActive = true,
                            IsStaff = false,
                            IsSuperUser = true,
                            LastLogin = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5857),
                            Name = "Admin",
                            Password = "$2a$11$pZHX2y7QdIXaEr.CLm6h/ObJegBir0rYQE5Jzm/MALyiwqzb11Uti",
                            RoleId = 1,
                            UpdatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5860)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5892),
                            DateJoined = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5893),
                            Email = "long@email.com",
                            IsActive = true,
                            IsStaff = false,
                            IsSuperUser = false,
                            LastLogin = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5893),
                            Name = "Long",
                            Password = "$2a$11$pZHX2y7QdIXaEr.CLm6h/ObJegBir0rYQE5Jzm/MALyiwqzb11Uti",
                            RoleId = 2,
                            UpdatedAt = new DateTime(2024, 5, 25, 10, 17, 33, 824, DateTimeKind.Utc).AddTicks(5894)
                        });
                });

            modelBuilder.Entity("DotnetBoilerplate.Domain.Models.User", b =>
                {
                    b.HasOne("DotnetBoilerplate.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DotnetBoilerplate.Domain.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
