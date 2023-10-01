﻿// <auto-generated />
using System;
using CloudStorage.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace cloud_storage.Migrations
{
    [DbContext(typeof(MsDatabaseContext))]
    [Migration("20230910070337_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CloudStorage.Entity.Dokument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Dokuments");
                });

            modelBuilder.Entity("CloudStorage.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DokumentUser", b =>
                {
                    b.Property<Guid>("AccessedDokumentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AllowedUsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccessedDokumentsId", "AllowedUsersId");

                    b.HasIndex("AllowedUsersId");

                    b.ToTable("DokumentUser");
                });

            modelBuilder.Entity("CloudStorage.Entity.Dokument", b =>
                {
                    b.HasOne("CloudStorage.Entity.User", "Owner")
                        .WithMany("CreatedDokuments")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DokumentUser", b =>
                {
                    b.HasOne("CloudStorage.Entity.Dokument", null)
                        .WithMany()
                        .HasForeignKey("AccessedDokumentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CloudStorage.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("AllowedUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CloudStorage.Entity.User", b =>
                {
                    b.Navigation("CreatedDokuments");
                });
#pragma warning restore 612, 618
        }
    }
}
