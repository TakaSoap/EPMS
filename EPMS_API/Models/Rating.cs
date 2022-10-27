using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models;

public class Rating
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string Id { get; set; }

    [BsonElement("user_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    [BsonElement("post_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string PostId { get; set; }

    [BsonElement("score")]
    public double Score { get; set; }

}