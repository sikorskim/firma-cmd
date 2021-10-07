using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using firma_mvc.Models;
using firma_mvc;

namespace firma_mvc.Data
{
    public class ApplicationDbContext : DbContext//: IdentityDbContext<ApplicationUser>
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //    : base(options)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}

        public ApplicationDbContext()
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Contractor> Contractor { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public DbSet<CarCostType> CarCostType { get; set; }
        public DbSet<CarCost> CarCost { get; set; }

        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceCorrection> InvoiceCorrections { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        public DbSet<Item> Item { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<VAT> VAT { get; set; }

        public DbSet<TaxBook> TaxBookItem { get; set; }
        public DbSet<FixedAssets> FixedAssets { get; set; }
        public DbSet<VATRegisterSell> VATRegisterSell { get; set; }
        public DbSet<VATRegisterBuy> VATRegisterBuy { get; set; }
        public DbSet<IncomeTax> IncomeTax { get; set; }
        public DbSet<VAT7> VAT7 { get; set; }
    }
}
