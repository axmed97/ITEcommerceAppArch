using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework;

public sealed class AppDbContext :  DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = ASUS; Database = ITEcommerceAppArchDb; Trusted_Connection = True; TrustServerCertificate = True;");
    }

    public DbSet<Product> Products { get; set; }
}
