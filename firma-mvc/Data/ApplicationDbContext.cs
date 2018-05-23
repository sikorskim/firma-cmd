using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using firma_mvc.Models;
using firma_mvc;

namespace firma_mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Contractor> Contractor { get; set; }

        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        public DbSet<Item> Item { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<VAT> VAT { get; set; }

        public DbSet<TaxBook> TaxBookItem { get; set; }
        public DbSet<FixedAssets> FixedAssets { get; set; }
        public DbSet<firma_mvc.VATRegisterSell> VATRegisterSell { get; set; }
        public DbSet<firma_mvc.VATRegisterBuy> VATRegisterBuy { get; set; }
    }
}
