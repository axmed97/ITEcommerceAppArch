using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IUploadService _uploadService;

    public UploadController(IUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, string folderName)
    {
        var result = await _uploadService.UploadFile(file, folderName);
        return Ok(result);
    }

    [HttpPost("upload-files")]
    public async Task<IActionResult> Uploads(IFormFileCollection files, string folderName)
    {
        var result = await _uploadService.UploadFiles(files, folderName);
        return Ok(result);
    }
}
