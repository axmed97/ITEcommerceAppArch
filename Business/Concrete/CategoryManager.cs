using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDAL _categoryDAL;

    public CategoryManager(ICategoryDAL categoryDAL)
    {
        _categoryDAL = categoryDAL;
    }

    public async Task CreateAsync(CreateCategoryDTO entity)
    {
        Category model = new()
        {
            Name = entity.Name,
            CreatedDate = DateTime.Now,
            IsDeleted = false
        };

        await _categoryDAL.AddAsync(model);
    }

    public GetCategoryDTO Get(Guid id)
    {
        var category = _categoryDAL.Get(x => x.Id == id, tracking: false);

        GetCategoryDTO model = new()
        {
            Id = category.Id,
            Name = category.Name
        };

        return model;
    }

    public List<GetCategoryDTO> GetAll()
    {
        var categories = _categoryDAL.GetAll(tracking: false);

        List<GetCategoryDTO> models = categories.Select(x => new GetCategoryDTO()
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return models;
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

    public async Task UpdateAsync(UpdateCategoryDTO entity)
    {
        var category = _categoryDAL.GetById(entity.Id);

        category.Name = entity.Name;
        category.UpdatedDate = DateTime.Now;

        await _categoryDAL.UpdateAsync(category);
    }
}
