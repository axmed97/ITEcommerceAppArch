using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework;

public sealed class AppDbContext :  IdentityDbContext<AppUser, AppRole, string>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = ASUS; Database = ITEcommerceAppArchDb; Trusted_Connection = True; TrustServerCertificate = True;");
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDeleted == false);
    }
}
