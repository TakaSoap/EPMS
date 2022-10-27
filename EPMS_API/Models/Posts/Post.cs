using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models.Posts;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("post_time")]
    public long PostTime { get; set; }

    [BsonElement("post_content")]
    public string PostContent { get; set; }

    [BsonElement("file_info")]
    public PostFileInfo[] FileInfo { get; set; }

}