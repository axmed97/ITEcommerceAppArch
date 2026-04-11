using Entities.Concrete;
using Entities.Concrete.Common;
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
    public DbSet<ProductPhoto> ProductPhotos { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasQueryFilter(x => x.IsDeleted == false);
        modelBuilder.Entity<Product>().HasQueryFilter(x => x.IsDeleted == false);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.Now;
            } else if (entry.State == EntityState.Modified)
            {
                if (entry.Property(x => x.IsDeleted).CurrentValue == true)
                {
                    entry.Entity.DeletedDate = DateTime.Now;
                }
                else
                {
                    entry.Entity.UpdatedDate = DateTime.Now;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
