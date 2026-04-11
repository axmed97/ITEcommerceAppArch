using Core.DataAccess.EntityFramework;
using Core.Utilities.Helper;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductsDTOs;
using Microsoft.EntityFrameworkCore;
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

    public PagedList<GetProductDTO> GetAllProduct(int pageSize, int currentPage)
    {
        using AppDbContext context = new();

        var products = context.Products
            .Include(x => x.Category)
            .Include(x => x.ProductPhotos)
            .Include(x => x.ProductColors)
            .ThenInclude(x => x.Color)
            .Skip((currentPage - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToList();

        List<GetProductDTO> models = products.Select(x => new GetProductDTO()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Discount = x.Discount,
            Count = x.Count,
            CategoryId = x.CategoryId,
            CategoryName = x.Category.Name,
            PhotoUrls = x.ProductPhotos.Select(y => y.PhotoUrl).ToList(),
            Colors = x.ProductColors.Select(y => new GetProductColorDTO()
            {
                ColorCode = y.Color.Code,
                ColorName = y.Color.Name,
                ColorId = y.Color.Id
            }).ToList()
        }).ToList();

        var productCount = context.Products.Count();

        PagedList<GetProductDTO> pagedList = new(models, productCount, currentPage, pageSize);
        return pagedList;
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductDTO entity)
    {
        await using AppDbContext context = new();
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var productDb = await context.Products.FindAsync(id);
            productDb!.Count = entity.Count;
            productDb.Name = entity.Name;
            productDb.Price = entity.Price;
            productDb.CategoryId = entity.CategoryId;
            productDb.Discount = entity.Discount;

            var productColors = context.ProductColors.Where(x => x.ProductId == id).ToList();
            context.RemoveRange(productColors);


            for (int i = 0; i < entity.ColorIds.Count; i++)
            {
                ProductColor productColor = new()
                {
                    ColorId = entity.ColorIds[i],
                    ProductId = id
                };
                await context.AddAsync(productColor);
            }

            var productPhotos = await context.ProductPhotos.Where(x => x.ProductId == id).ToListAsync();
            context.RemoveRange(productPhotos);

            for (int i = 0; i < entity.PhotoUrls.Count; i++)
            {
                ProductPhoto productPhoto = new()
                {
                    PhotoUrl = entity.PhotoUrls[i],
                    ProductId = id
                };
                await context.ProductPhotos.AddAsync(productPhoto);
            }

            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }

    //public async new Task AddAsync(Product product)
    //{
    //    await using AppDbContext context = new();
    //    await context.AddAsync(product);
    //    await context.SaveChangesAsync();
    //}
}
