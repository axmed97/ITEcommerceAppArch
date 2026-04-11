using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.ProductsDTOs;

namespace DataAccess.Abstract;

public interface IProductDAL : IRepositoryBase<Product>
{
    Task CreateProductAsync(AddProductDTO entity);
    //new Task AddAsync(Product product);
}
