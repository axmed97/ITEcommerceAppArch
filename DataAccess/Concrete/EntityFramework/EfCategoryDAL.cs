using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework;

public class EfCategoryDAL : EfRepositoryBase<Category, AppDbContext>, ICategoryDAL
{
    public Category GetFromRecycleBin(Guid id)
    {
        using AppDbContext context = new();

        var model = context.Categories.IgnoreQueryFilters().AsNoTracking().FirstOrDefault(x => x.Id == id);
        return model;
    }
}
