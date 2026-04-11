using Business.Abstract;
using Entities.DTOs.UploadDTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete;

public class UploadManager : IUploadService
{
    private readonly IWebHostEnvironment _env;

    public UploadManager(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<UploadDTO> UploadFile(IFormFile file, string folderName)
    {
        string uploadPath = Path.Combine(_env.WebRootPath, folderName).ToLower();
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }
        var path = $"/{folderName}/" + Guid.NewGuid() + file.FileName;
        using FileStream fileStream = new(_env.WebRootPath + path, FileMode.Create);
        await file.CopyToAsync(fileStream);

        return new()
        {
            FileName = file.FileName,
            Path = path
        };
    }

    public async Task<List<UploadDTO>> UploadFiles(IFormFileCollection files, string folderName)
    {
        List<UploadDTO> uploads = new();

        foreach (var file in files)
        {
            UploadDTO data = await UploadFile(file, folderName);
            uploads.Add(data);
        }

        return uploads;
    }
}
