using Core.DataAccess;
using Core.Utilities.Helper;
using Entities.Concrete;
using Entities.DTOs.ProductsDTOs;

namespace DataAccess.Abstract;

public interface IProductDAL : IRepositoryBase<Product>
{
    Task CreateProductAsync(AddProductDTO entity);
    //new Task AddAsync(Product product);
    Task UpdateProductAsync(Guid id, UpdateProductDTO entity);
    PagedList<GetProductDTO> GetAllProduct(int pageSize, int currentPage); 
}
