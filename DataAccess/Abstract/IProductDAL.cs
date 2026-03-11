using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IProductDAL : IRepositoryBase<Product>
{
    Task CreateTogrulVersionAsync();
}
