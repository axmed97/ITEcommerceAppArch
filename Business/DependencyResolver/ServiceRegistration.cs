using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolver;

public static class ServiceRegistration
{
    // IoC - Invertion of Containers
    public static void AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IProductDAL, EfProductDAL>();

        services.AddScoped<ICategoryDAL, EfCategoryDAL>();
    }
}
