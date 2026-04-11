using Entities.DTOs.UploadDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract;

public interface IUploadService
{
    Task<UploadDTO> UploadFile(IFormFile file, string folderName);
    Task<List<UploadDTO>> UploadFiles(IFormFileCollection files, string folderName);
}
