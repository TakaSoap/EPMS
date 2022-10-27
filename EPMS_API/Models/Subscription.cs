using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models
{
    public class Subscription
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("subscribed_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SubscribedId { get; set; }

        [BsonElement("subscriber_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SubscriberId { get; set; }
    }
}
