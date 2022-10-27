using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models.Posts;

public class PostFileInfo
{
    [BsonElement("file_name")]
    public string fileName { get; set; }

    [BsonElement("file_type")]
    public string fileType { get; set; }

    [BsonElement("file_path")]
    public string filePath { get; set; }
}