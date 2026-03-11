using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDAL : EfRepositoryBase<Product, AppDbContext>, IProductDAL
{
    public async Task CreateTogrulVersionAsync()
    {
        using AppDbContext context = new();

        await context.Products.AddAsync(new Product
        {
            Name = "Asus"
        });

        await context.SaveChangesAsync();
    }
}
