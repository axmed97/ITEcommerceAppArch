using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret;
using Core.Utilities.Results.Concret.ErrorResults;
using Core.Utilities.Results.Concret.SuccessResults;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDAL _categoryDAL;
    private readonly IMemoryCache _memoryCache;
    public CategoryManager(ICategoryDAL categoryDAL, IMemoryCache memoryCache)
    {
        _categoryDAL = categoryDAL;
        _memoryCache = memoryCache;
    }

    public async Task<IResult> CreateAsync(CreateCategoryDTO entity)
    {
        Category model = new()
        {
            Name = entity.Name,
            //CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        await _categoryDAL.AddAsync(model);

        _memoryCache.Remove("category");


        return new SuccessResult(HttpStatusCode.Created, "Created");
        //return new Result(HttpStatusCode.Created, true, "Created");
    }

    public IDataResult<GetCategoryDTO> Get(Guid id)
    {
        var category = _categoryDAL.Get(x => x.Id == id, tracking: false);

        if (category == null)
            return new ErrorDataResult<GetCategoryDTO>(HttpStatusCode.NotFound);

        GetCategoryDTO model = new()
        {
            Id = category.Id,
            Name = category.Name
        };

        return new SuccessDataResult<GetCategoryDTO>(HttpStatusCode.OK, model);
    }

    public IDataResult<List<GetCategoryDTO>> GetAll()
    {
        List<Category> x = _memoryCache.Get<List<Category>>("category");
        if (x != null)
        {
            List<GetCategoryDTO> models = x.Select(x => new GetCategoryDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return new DataResult<List<GetCategoryDTO>>(models, HttpStatusCode.OK, true);
        }
        else
        {
            List<Category> categories = _categoryDAL.GetAll(tracking: false);

            _memoryCache.Set("category", categories, options: new()
            {
                AbsoluteExpiration =  DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

            List<GetCategoryDTO> models = categories.Select(x => new GetCategoryDTO()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return new DataResult<List<GetCategoryDTO>>(models, HttpStatusCode.OK, true);
        }

        
    }

    public GetCategoryDTO GetFromRecycleBin(Guid id)
    {
        var category = _categoryDAL.GetFromRecycleBin(id);

        GetCategoryDTO model = new()
        {
            Id = category.Id,
            Name = category.Name
        };

        return model;
    }

    public async Task HardDeleteAsync(Guid id)
    {
        var model = _categoryDAL.GetById(id);
        await _categoryDAL.DeleteAsync(model);
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var model = _categoryDAL.GetById(id);

        //model.DeletedDate = DateTime.Now;
        model.IsDeleted = true;
        await _categoryDAL.UpdateAsync(model);
    }

    public async Task<IResult> UpdateAsync(UpdateCategoryDTO entity)
    {
        var category = _categoryDAL.GetById(entity.Id);
        if (category == null)
            return new ErrorResult(HttpStatusCode.NotFound, "Category not found!");

        category.Name = entity.Name;
        //category.UpdatedDate = DateTime.Now;

        await _categoryDAL.UpdateAsync(category);

        return new Result(HttpStatusCode.OK, true);
    }
}
