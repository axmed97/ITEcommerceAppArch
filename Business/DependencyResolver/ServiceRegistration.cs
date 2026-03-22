using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolver;

public static class ServiceRegistration
{
    // IoC - Invertion of Containers
    public static void AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<AppDbContext>();

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IProductDAL, EfProductDAL>();

        services.AddScoped<ICategoryDAL, EfCategoryDAL>();
        services.AddScoped<ICategoryService, CategoryManager>();

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<ITokenService, TokenManager>();
    }
}
