using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret;
using Core.Utilities.Results.Concret.ErrorResults;
using Core.Utilities.Results.Concret.SuccessResults;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System.Net;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDAL _categoryDAL;

    public CategoryManager(ICategoryDAL categoryDAL)
    {
        _categoryDAL = categoryDAL;
    }

    public async Task<IResult> CreateAsync(CreateCategoryDTO entity)
    {
        Category model = new()
        {
            Name = entity.Name,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        await _categoryDAL.AddAsync(model);


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
        var categories = _categoryDAL.GetAll(tracking: false);

        List<GetCategoryDTO> models = categories.Select(x => new GetCategoryDTO()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return new DataResult<List<GetCategoryDTO>>(models, HttpStatusCode.OK, true);
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

        model.DeletedDate = DateTime.Now;
        model.IsDeleted = true;
        await _categoryDAL.UpdateAsync(model);
    }

    public async Task<IResult> UpdateAsync(UpdateCategoryDTO entity)
    {
        var category = _categoryDAL.GetById(entity.Id);
        if (category == null)
            return new ErrorResult(HttpStatusCode.NotFound, "Category not found!");

        category.Name = entity.Name;
        category.UpdatedDate = DateTime.Now;

        await _categoryDAL.UpdateAsync(category);

        return new Result(HttpStatusCode.OK, true);
    }
}
