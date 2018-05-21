﻿// <auto-generated />
using firma_mvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace firmamvc.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180521123153_migr2")]
    partial class migr2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("firma_mvc.Contractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BuldingNo");

                    b.Property<string>("City");

                    b.Property<string>("Community");

                    b.Property<string>("CountryCode");

                    b.Property<string>("County");

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("NIP");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("PostOffice");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Street");

                    b.Property<string>("Voivodeship");

                    b.HasKey("Id");

                    b.ToTable("Contractor");
                });

            modelBuilder.Entity("firma_mvc.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContractorId");

                    b.Property<DateTime>("DateOfIssue");

                    b.Property<int>("InvoiceStatusId");

                    b.Property<string>("Number");

                    b.Property<int>("PaymentMethodId");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("InvoiceStatusId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("firma_mvc.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvoiceId");

                    b.Property<int>("ItemId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ItemId");

                    b.ToTable("InvoiceItem");
                });

            modelBuilder.Entity("firma_mvc.InvoiceStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("InvoiceStatus");
                });

            modelBuilder.Entity("firma_mvc.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("UnitOfMeasureId");

                    b.Property<int>("VATId");

                    b.HasKey("Id");

                    b.HasIndex("UnitOfMeasureId");

                    b.HasIndex("VATId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("firma_mvc.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("firma_mvc.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DueTerm");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("firma_mvc.TaxBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<decimal>("BuysSideEffects");

                    b.Property<decimal>("Column15");

                    b.Property<string>("Comments");

                    b.Property<string>("CostDescription");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<decimal>("GoodsBuys");

                    b.Property<string>("InvoiceNumber");

                    b.Property<string>("NIP");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.Property<decimal>("OtherCosts");

                    b.Property<decimal>("OtherIncome");

                    b.Property<decimal>("ResearchCostValue");

                    b.Property<decimal>("Salary");

                    b.Property<decimal>("SellValue");

                    b.HasKey("Id");

                    b.ToTable("TaxBookItem");
                });

            modelBuilder.Entity("firma_mvc.UnitOfMeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("UnitOfMeasure");
                });

            modelBuilder.Entity("firma_mvc.VAT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.ToTable("VAT");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("firma_mvc.Invoice", b =>
                {
                    b.HasOne("firma_mvc.Contractor", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("firma_mvc.InvoiceStatus", "InvoiceStatus")
                        .WithMany()
                        .HasForeignKey("InvoiceStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("firma_mvc.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("firma_mvc.InvoiceItem", b =>
                {
                    b.HasOne("firma_mvc.Invoice", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("firma_mvc.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("firma_mvc.Item", b =>
                {
                    b.HasOne("firma_mvc.UnitOfMeasure", "UnitOfMeasure")
                        .WithMany()
                        .HasForeignKey("UnitOfMeasureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("firma_mvc.VAT", "VAT")
                        .WithMany()
                        .HasForeignKey("VATId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("firma_mvc.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("firma_mvc.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("firma_mvc.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("firma_mvc.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
