using Core.Utilities.Helper;
using Core.Utilities.Results.Abstract;
using Entities.DTOs.ProductsDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract;

public interface IProductService
{
    Task<IResult> CreateAsync(AddProductDTO entity);
    Task<IResult> UpdateAsync(Guid id, UpdateProductDTO entity);
    IDataResult<PagedList<GetProductDTO>> GetAll(int pageSize, int currentPage);
}
