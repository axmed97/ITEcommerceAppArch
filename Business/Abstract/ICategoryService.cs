using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace Business.Abstract;

public interface ICategoryService
{
    Task CreateAsync(CreateCategoryDTO entity);
    GetCategoryDTO Get(Guid id);
    GetCategoryDTO GetFromRecycleBin(Guid id);
    List<GetCategoryDTO> GetAll();
    Task UpdateAsync(UpdateCategoryDTO entity);
    Task HardDeleteAsync(Guid id);
    Task SoftDeleteAsync(Guid id);
}
