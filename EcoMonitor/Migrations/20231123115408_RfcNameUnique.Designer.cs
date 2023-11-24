﻿// <auto-generated />
using EcoMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoMonitor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231123115408_RfcNameUnique")]
    partial class RfcNameUnique
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EcoMonitor.Model.Company", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("location")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("companies");
                });

            modelBuilder.Entity("EcoMonitor.Model.EnvFactor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("factor_Ca_value")
                        .HasColumnType("double");

                    b.Property<double>("factor_Ch_value")
                        .HasColumnType("double");

                    b.Property<string>("factor_Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<double>("factor_value")
                        .HasColumnType("double");

                    b.Property<int>("passport_id")
                        .HasColumnType("int");

                    b.Property<int>("rfc_factor_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("passport_id");

                    b.HasIndex("rfc_factor_id");

                    b.HasIndex("factor_Name", "passport_id")
                        .IsUnique();

                    b.ToTable("env_Factors");
                });

            modelBuilder.Entity("EcoMonitor.Model.Passport", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("company_id")
                        .HasColumnType("int");

                    b.Property<string>("data2")
                        .HasColumnType("longtext");

                    b.Property<string>("data3")
                        .HasColumnType("longtext");

                    b.Property<string>("data4")
                        .HasColumnType("longtext");

                    b.Property<string>("data5")
                        .HasColumnType("longtext");

                    b.Property<int>("year")
                        .HasColumnType("YEAR(4)");

                    b.HasKey("id");

                    b.HasIndex("company_id");

                    b.HasIndex("year", "company_id")
                        .IsUnique();

                    b.ToTable("passports");
                });

            modelBuilder.Entity("EcoMonitor.Model.RfcFactor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("factor_Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<double>("factor_value")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.HasIndex("factor_Name")
                        .IsUnique();

                    b.ToTable("rfc_Factors");
                });

            modelBuilder.Entity("EcoMonitor.Model.EnvFactor", b =>
                {
                    b.HasOne("EcoMonitor.Model.Passport", "Passport")
                        .WithMany()
                        .HasForeignKey("passport_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.RfcFactor", "RfcFactor")
                        .WithMany()
                        .HasForeignKey("rfc_factor_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passport");

                    b.Navigation("RfcFactor");
                });

            modelBuilder.Entity("EcoMonitor.Model.Passport", b =>
                {
                    b.HasOne("EcoMonitor.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("company_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}