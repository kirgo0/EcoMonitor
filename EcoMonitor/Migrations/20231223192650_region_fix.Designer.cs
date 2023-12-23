﻿// <auto-generated />
using System;
using EcoMonitor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcoMonitor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231223192650_region_fix")]
    partial class region_fix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CompanyNews", b =>
                {
                    b.Property<int>("companiesid")
                        .HasColumnType("int");

                    b.Property<int>("newsid")
                        .HasColumnType("int");

                    b.HasKey("companiesid", "newsid");

                    b.HasIndex("newsid");

                    b.ToTable("CompanyNews");
                });

            modelBuilder.Entity("EcoMonitor.Model.City", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("isResort")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("population")
                        .HasColumnType("int");

                    b.Property<int>("region_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.HasIndex("region_id");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("EcoMonitor.Model.Company", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("city_id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("id");

                    b.HasIndex("city_id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("companies");
                });

            modelBuilder.Entity("EcoMonitor.Model.CompanyWaste", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ko")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Koc")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Kt")
                        .HasColumnType("double");

                    b.Property<int>("passport_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("passport_id");

                    b.ToTable("company_wastes");
                });

            modelBuilder.Entity("EcoMonitor.Model.FormattedNews", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("authors")
                        .HasColumnType("longtext");

                    b.Property<string>("body")
                        .HasColumnType("longtext");

                    b.Property<string>("company_names")
                        .HasColumnType("longtext");

                    b.Property<int>("likes")
                        .HasColumnType("int");

                    b.Property<DateTime>("post_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("region_names")
                        .HasColumnType("longtext");

                    b.Property<string>("source_url")
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("update_date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable((string)null);

                    b.ToView("formatted_news", (string)null);
                });

            modelBuilder.Entity("EcoMonitor.Model.News", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("body")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("post_date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("source_url")
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("update_date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.HasIndex("title")
                        .IsUnique();

                    b.ToTable("news");
                });

            modelBuilder.Entity("EcoMonitor.Model.Passport", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("company_id")
                        .HasColumnType("int");

                    b.Property<double>("source_operating_time")
                        .HasColumnType("double");

                    b.Property<short>("year")
                        .HasColumnType("YEAR(4)");

                    b.HasKey("id");

                    b.HasIndex("company_id");

                    b.HasIndex("year", "company_id")
                        .IsUnique();

                    b.ToTable("passports");
                });

            modelBuilder.Entity("EcoMonitor.Model.Pollutant", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("GDK_value")
                        .HasColumnType("double");

                    b.Property<double>("RFC_value")
                        .HasColumnType("double");

                    b.Property<double?>("SF_value")
                        .HasColumnType("double");

                    b.Property<string>("damaged_organs")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<double?>("mass_flow_rate")
                        .HasColumnType("double");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("pollutants");
                });

            modelBuilder.Entity("EcoMonitor.Model.Pollution", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("CA_value")
                        .HasColumnType("double");

                    b.Property<double>("CH_value")
                        .HasColumnType("double");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("passport_id")
                        .HasColumnType("int");

                    b.Property<int?>("pollutant_id")
                        .HasColumnType("int");

                    b.Property<double?>("radioactive_disposal_time")
                        .HasColumnType("double");

                    b.Property<double?>("radioactive_volume")
                        .HasColumnType("double");

                    b.Property<double>("value")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.HasIndex("passport_id");

                    b.HasIndex("pollutant_id");

                    b.HasIndex("name", "passport_id")
                        .IsUnique();

                    b.ToTable("pollutions");
                });

            modelBuilder.Entity("EcoMonitor.Model.Region", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("population")
                        .HasColumnType("int");

                    b.Property<double?>("square")
                        .HasColumnType("double");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.ToTable("regions");
                });

            modelBuilder.Entity("EcoMonitor.Model.TaxNorm", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("air_emissions")
                        .HasColumnType("double");

                    b.Property<double>("disposal_of_wastes")
                        .HasColumnType("double");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int?>("pollutant_id")
                        .HasColumnType("int");

                    b.Property<double>("radioactive_wastes")
                        .HasColumnType("double");

                    b.Property<double>("temporary_disposal_of_radioactive_wastes")
                        .HasColumnType("double");

                    b.Property<double>("water_emissions")
                        .HasColumnType("double");

                    b.Property<short>("year")
                        .HasColumnType("YEAR(4)");

                    b.HasKey("id");

                    b.HasIndex("pollutant_id");

                    b.HasIndex("year", "name")
                        .IsUnique();

                    b.ToTable("tax_norms");
                });

            modelBuilder.Entity("EcoMonitor.Model.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NewsRegion", b =>
                {
                    b.Property<int>("newsid")
                        .HasColumnType("int");

                    b.Property<int>("regionsid")
                        .HasColumnType("int");

                    b.HasKey("newsid", "regionsid");

                    b.HasIndex("regionsid");

                    b.ToTable("NewsRegion");
                });

            modelBuilder.Entity("NewsUser", b =>
                {
                    b.Property<string>("authorsId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("newsid")
                        .HasColumnType("int");

                    b.HasKey("authorsId", "newsid");

                    b.HasIndex("newsid");

                    b.ToTable("NewsUser");
                });

            modelBuilder.Entity("NewsUser1", b =>
                {
                    b.Property<string>("followersId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("likedNewsid")
                        .HasColumnType("int");

                    b.HasKey("followersId", "likedNewsid");

                    b.HasIndex("likedNewsid");

                    b.ToTable("NewsFollowers", (string)null);
                });

            modelBuilder.Entity("CompanyNews", b =>
                {
                    b.HasOne("EcoMonitor.Model.Company", null)
                        .WithMany()
                        .HasForeignKey("companiesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.News", null)
                        .WithMany()
                        .HasForeignKey("newsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EcoMonitor.Model.City", b =>
                {
                    b.HasOne("EcoMonitor.Model.Region", "Region")
                        .WithMany()
                        .HasForeignKey("region_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("EcoMonitor.Model.Company", b =>
                {
                    b.HasOne("EcoMonitor.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("city_id");

                    b.Navigation("City");
                });

            modelBuilder.Entity("EcoMonitor.Model.CompanyWaste", b =>
                {
                    b.HasOne("EcoMonitor.Model.Passport", "Passport")
                        .WithMany()
                        .HasForeignKey("passport_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passport");
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

            modelBuilder.Entity("EcoMonitor.Model.Pollution", b =>
                {
                    b.HasOne("EcoMonitor.Model.Passport", "Passport")
                        .WithMany()
                        .HasForeignKey("passport_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.Pollutant", "Pollutant")
                        .WithMany()
                        .HasForeignKey("pollutant_id");

                    b.Navigation("Passport");

                    b.Navigation("Pollutant");
                });

            modelBuilder.Entity("EcoMonitor.Model.TaxNorm", b =>
                {
                    b.HasOne("EcoMonitor.Model.Pollutant", "Pollutant")
                        .WithMany()
                        .HasForeignKey("pollutant_id");

                    b.Navigation("Pollutant");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EcoMonitor.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EcoMonitor.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EcoMonitor.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewsRegion", b =>
                {
                    b.HasOne("EcoMonitor.Model.News", null)
                        .WithMany()
                        .HasForeignKey("newsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.Region", null)
                        .WithMany()
                        .HasForeignKey("regionsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewsUser", b =>
                {
                    b.HasOne("EcoMonitor.Model.User", null)
                        .WithMany()
                        .HasForeignKey("authorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.News", null)
                        .WithMany()
                        .HasForeignKey("newsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewsUser1", b =>
                {
                    b.HasOne("EcoMonitor.Model.User", null)
                        .WithMany()
                        .HasForeignKey("followersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcoMonitor.Model.News", null)
                        .WithMany()
                        .HasForeignKey("likedNewsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
