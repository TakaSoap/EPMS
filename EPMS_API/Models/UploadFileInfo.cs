namespace EPMS_API.Models;

public class UploadFileInfo
{
    public string fileName { get; set; }
    public string filePath { get; set; }
    public string fileType { get; set; }

    public UploadFileInfo(string fileName, string filePath, string fileType)
    {
        this.fileName = fileName;
        this.filePath = filePath;
        this.fileType = fileType;
    }
}