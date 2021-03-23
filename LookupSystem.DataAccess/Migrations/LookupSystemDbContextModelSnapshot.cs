﻿// <auto-generated />
using System;
using LookupSystem.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LookupSystem.DataAccess.Migrations
{
    [DbContext(typeof(LookupSystemDbContext))]
    partial class LookupSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LookupSystem.DataAccess.Models.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LookupSystem.DataAccess.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Fired")
                        .HasColumnType("bit");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LookupSystem.DataAccess.Models.UserContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("City")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Country")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeleteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverLicense")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("MobilePhone")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(30)");

                    b.Property<string>("SSN")
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex("Phone", "Email");

                    b.ToTable("UserContacts");
                });

            modelBuilder.Entity("TagUser", b =>
                {
                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UsersTags");
                });

            modelBuilder.Entity("LookupSystem.DataAccess.Models.UserContact", b =>
                {
                    b.HasOne("LookupSystem.DataAccess.Models.User", "User")
                        .WithOne("UserContact")
                        .HasForeignKey("LookupSystem.DataAccess.Models.UserContact", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TagUser", b =>
                {
                    b.HasOne("LookupSystem.DataAccess.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LookupSystem.DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LookupSystem.DataAccess.Models.User", b =>
                {
                    b.Navigation("UserContact");
                });
#pragma warning restore 612, 618
        }
    }
}
