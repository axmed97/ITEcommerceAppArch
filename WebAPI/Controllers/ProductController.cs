using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    // Life cycle -  Singleton, Scoped, Transiet
    //private readonly ITestService _testService;// ItestService t1 = new TestManager();
    //private readonly ITestService _testService2;// ItestService t2 = new TestManager();
    //public ProductController(ITestService testService, ITestService testService2)
    //{
    //    _testService = testService;
    //    _testService2 = testService2;
    //}

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult X()
    {
        return Ok();
    }

   



}
