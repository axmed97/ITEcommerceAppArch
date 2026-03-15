using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract;

public interface ICategoryDAL : IRepositoryBase<Category>
{
    Category GetFromRecycleBin(Guid id);
}
