using Microsoft.AspNetCore.Mvc;
using EPMS_API.Models;

namespace EPMS_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private bool CheckIfImage(IFormFile file)
    {
        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
        return (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".webp"); // Change the extension based on your need
    }

    [HttpPost]
    public async Task<UploadFileInfo> UploadFile(IFormFile file)
    {
        string fileName;
        string fileType;
        string filePath;

        if (CheckIfImage(file))
        {
            fileType = "image";
        }
        else
        {
            fileType = "file";
        }

        try
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            fileName = DateTimeOffset.Now.ToUnixTimeSeconds() + "_" + file.FileName; //Create a new Name for the file due to security reasons.

            //var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

            //if (!Directory.Exists(pathBuilt))
            //{
            //    Directory.CreateDirectory(pathBuilt);
            //}

            //filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
            //       fileName);

            filePath = Path.Combine("/www/wwwroot/epms.takasoap.xyz/files",
                fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        catch
        {
            return new UploadFileInfo("", "", "error");
        }

        return new UploadFileInfo(fileName, Path.Combine("/files", fileName), fileType);
    }
}