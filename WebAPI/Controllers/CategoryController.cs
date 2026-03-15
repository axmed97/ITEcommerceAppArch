using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        await _categoryService.CreateAsync(entity);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        var model = _categoryService.Get(Guid.Parse(id));
        return Ok(model);
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
        await _categoryService.UpdateAsync(entity);
        return Ok();
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
        var models = _categoryService.GetAll();
        return Ok(models);
    }
}
