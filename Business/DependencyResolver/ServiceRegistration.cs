using Business.Abstract;
using Business.Concrete;
using Business.Validators.AuthValidators;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.AuthDTOs;
using FluentValidation;
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

        services.AddMemoryCache();

        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IProductDAL, EfProductDAL>();

        services.AddScoped<ICategoryDAL, EfCategoryDAL>();
        services.AddScoped<ICategoryService, CategoryManager>();

        services.AddScoped<IColorDAL, EfColorDAL>();
        services.AddScoped<IColorService, ColorManager>();

        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<ITokenService, TokenManager>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUploadService, UploadManager>();



        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("az");
    }
}
