using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.ColorDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColorController : ControllerBase
{
    private readonly IColorService colorService;

    public ColorController(IColorService colorService)
    {
        this.colorService = colorService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddColorDTO color)
    {
        var result = await colorService.CreateAsync(color);
        return StatusCode((int)result.StatusCode, result);
    }
}
