
using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concret.ErrorResults;
using Core.Utilities.Results.Concret.SuccessResults;
using Entities.Concrete;
using Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Identity;

namespace Business.Concrete;

public class RoleService(RoleManager<AppRole> roleManager) : IRoleService
{

    public async Task<IResult> CreateAsync(AddRoleDTO entity)
    {
        await roleManager.CreateAsync(new AppRole()
        {
            Name = entity.Name
        });

        return new SuccessResult(System.Net.HttpStatusCode.Created);
    }
}
