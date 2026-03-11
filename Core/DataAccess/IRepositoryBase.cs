using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess;

// Repository Design Pattern
public interface IRepositoryBase<TEntity>
    where TEntity : class, IEntity
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity);
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity);
    // Func, Predicate, Action Hazir Delegate
    List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, bool tracking = true);
    TEntity? Get(Expression<Func<TEntity, bool>>? expression = null, bool tracking = true);
    TEntity? GetById(Guid id);
}
