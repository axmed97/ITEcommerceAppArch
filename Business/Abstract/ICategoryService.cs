using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;

namespace Business.Abstract;

public interface ICategoryService
{
    Task<IResult> CreateAsync(CreateCategoryDTO entity);
    IDataResult<GetCategoryDTO> Get(Guid id);
    GetCategoryDTO GetFromRecycleBin(Guid id);
    IDataResult<List<GetCategoryDTO>> GetAll();
    Task<IResult> UpdateAsync(UpdateCategoryDTO entity);
    Task HardDeleteAsync(Guid id);
    Task SoftDeleteAsync(Guid id);
}
