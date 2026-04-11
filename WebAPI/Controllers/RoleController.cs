using Business.Abstract;
using Business.Concrete;
using Entities.DTOs.RoleDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController(IRoleService roleService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateAsync(AddRoleDTO entity)
    {
        
        var result = await roleService.CreateAsync(entity);
        return StatusCode((int)result.StatusCode, result);
    }
}
