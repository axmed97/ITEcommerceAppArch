using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductsDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework;

public class EfProductDAL : EfRepositoryBase<Product, AppDbContext>, IProductDAL
{
    public async Task CreateProductAsync(AddProductDTO entity)
    {
        await using AppDbContext context = new();

        Product product = new()
        {
            Name = entity.Name,
            Price = entity.Price,
            Discount = entity.Discount,
            Count = entity.Count,
            CategoryId = entity.CategoryId
        };

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        for (int i = 0; i < entity.ColorIds.Count; i++)
        {
            ProductColor productColor = new()
            {
                ProductId = product.Id,
                ColorId = entity.ColorIds[i]
            };
            await context.ProductColors.AddAsync(productColor);
        }

        await context.SaveChangesAsync();

        for (int i = 0; i < entity.PhotoUrls.Count; i++)
        {
            ProductPhoto productPhoto = new()
            {
                ProductId = product.Id,
                PhotoUrl = entity.PhotoUrls[i]
            };
            await context.ProductPhotos.AddAsync(productPhoto);
        }

        await context.SaveChangesAsync();
    }

    //public async new Task AddAsync(Product product)
    //{
    //    await using AppDbContext context = new();
    //    await context.AddAsync(product);
    //    await context.SaveChangesAsync();
    //}
}
