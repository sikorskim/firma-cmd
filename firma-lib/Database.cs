using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace firma_lib
{
   public class Database : DbContext
    {
            public DbSet<Contractor> Contractor { get; set; }
            public DbSet<VAT> VAT { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=SP-268\SQLEXPRESS;Initial Catalog=firma;User ID=sa;Password=Hutmen22;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
