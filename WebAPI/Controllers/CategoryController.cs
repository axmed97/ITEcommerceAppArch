using Business.Abstract;
using Core.Utilities.Results.Concret;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDTO entity)
    {
        var result = await _categoryService.CreateAsync(entity);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var result = _categoryService.Get(Guid.Parse(id));
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpGet("recycleBin/{id}")]
    public IActionResult GetByIdFromRecycleBin(string id)
    {
        var model = _categoryService.GetFromRecycleBin(Guid.Parse(id));
        return Ok(model);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryDTO entity)
    {
        var result = await _categoryService.UpdateAsync(entity);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        await _categoryService.SoftDeleteAsync(Guid.Parse(id));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> HardDelete(string id)
    {
        await _categoryService.HardDeleteAsync(Guid.Parse(id));
        return Ok();
    }
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _categoryService.GetAll();
        return StatusCode((int)result.StatusCode, result);
    }
}
