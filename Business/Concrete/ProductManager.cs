using Business.Abstract;
using Business.Validators.ProductValidators;
using Core.DataAccess;
using Core.Utilities.Helper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret.ErrorResults;
using Core.Utilities.Results.Concret.SuccessResults;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.ProductsDTOs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Business.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDAL _productDAL;
    private readonly ICategoryService _categoryService;
    private readonly IMemoryCache _memoryCache;
    public ProductManager(IProductDAL productDAL, ICategoryService categoryService, IMemoryCache memoryCache)
    {
        _productDAL = productDAL;
        _categoryService = categoryService;
        _memoryCache = memoryCache;
    }

    public async Task<IResult> CreateAsync(AddProductDTO entity)
    {
        var cat = _categoryService.Get(entity.CategoryId);
        if (cat == null)
        {
            return new ErrorResult(System.Net.HttpStatusCode.NotFound);
        }
        Product product = new()
        {
            Name = entity.Name,
            Price = entity.Price,
            Discount = entity.Discount,
            Count = entity.Count,
            CategoryId = entity.CategoryId,
            ProductPhotos = entity.PhotoUrls.Select(photo => new ProductPhoto()
            {
                PhotoUrl = photo,
            }).ToList(),
            ProductColors = entity.ColorIds.Select(color => new ProductColor()
            {
                ColorId = color
            }).ToList()
        };

        await _productDAL.AddAsync(product);
        // await _productDAL.CreateProductAsync(entity);
        return new SuccessResult(HttpStatusCode.Created);
    }

    public IDataResult<PagedList<GetProductDTO>> GetAll(int pageSize, int currentPage)
    {
        var products = _productDAL.GetAllProduct(pageSize, currentPage);
        return new SuccessDataResult<PagedList<GetProductDTO>>(HttpStatusCode.OK, products);
    }

    public async Task<IResult> UpdateAsync(Guid id, UpdateProductDTO entity)
    {
        var validator = new UpdateProductValidator();
        var validate = validator.Validate(entity);
        if (!validate.IsValid)
        {
            string message = string.Empty;
            foreach (var error in validate.Errors)
            {
                message += error.ErrorMessage;
            }
            return new ErrorResult(HttpStatusCode.BadRequest, message.ToString());
        }

        var product = _productDAL.Get(x => x.Id == id, tracking: false);
        
        if (product == null) return new ErrorResult(HttpStatusCode.NotFound);

        await _productDAL.UpdateProductAsync(id, entity);
        return new SuccessResult(HttpStatusCode.OK);
    }
}
