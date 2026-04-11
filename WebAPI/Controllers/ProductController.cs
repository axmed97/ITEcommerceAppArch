using Business.Abstract;
using Entities.DTOs.ProductsDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddProductDTO entity)
    {
        var result = await _productService.CreateAsync(entity);
        return StatusCode((int)result.StatusCode, result);
    }
}
