
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework;

public class EfRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
    where TEntity : class, IEntity
    where TContext : DbContext, new()
{
    public void Add(TEntity entity) // Product product // Category category
    {
        using TContext context = new();
        var addEntity = context.Entry(entity); // context.Products // context.Category
        addEntity.State = EntityState.Added; // add
        context.SaveChanges(); // save
    }

    public async Task AddAsync(TEntity entity)
    {
        using TContext context = new();
        var addEntity = context.Entry(entity); 
        addEntity.State = EntityState.Added;
        await context.SaveChangesAsync(); 
    }

    public void Delete(TEntity entity)
    {
        using TContext context = new();
        var deleteEntity = context.Entry(entity);
        deleteEntity.State = EntityState.Deleted;
        context.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        using TContext context = new();
        var deleteEntity = context.Entry(entity);
        deleteEntity.State = EntityState.Deleted;
        await context.SaveChangesAsync();
    }

    public TEntity? Get(Expression<Func<TEntity, bool>>? expression = null, bool tracking = true)
    {
        using TContext context = new();

        if (tracking)
            if (expression != null)
                return context.Set<TEntity>().FirstOrDefault(expression);
            else
                return context.Set<TEntity>().FirstOrDefault();
        else
            if (expression != null)
                return context.Set<TEntity>().AsNoTracking().FirstOrDefault(expression);
            else
                return context.Set<TEntity>().AsNoTracking().FirstOrDefault();
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? expression = null, bool tracking = true)
    {
        using TContext context = new();

        if (tracking)
            if (expression == null)
                return context.Set<TEntity>().ToList();
            else
                return context.Set<TEntity>().Where(expression).ToList();
        else
            if (expression == null)
                return context.Set<TEntity>().AsNoTracking().ToList();
            else
                return context.Set<TEntity>().Where(expression).AsNoTracking().ToList();
    }

    public TEntity? GetById(Guid id)
    {
        using TContext context = new();

        return context.Set<TEntity>().Find(id);
    }

    public void Update(TEntity entity)
    {
        using TContext context = new();
        var updateEntity = context.Entry(entity);
        updateEntity.State = EntityState.Modified;
        context.SaveChanges();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        using TContext context = new();
        var updateEntity = context.Entry(entity);
        updateEntity.State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}
