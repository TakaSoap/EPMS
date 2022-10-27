using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models.Comment;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("post_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PostId { get; set; }

    [BsonElement("comment_content")]
    public string CommentContent { get; set; }

}