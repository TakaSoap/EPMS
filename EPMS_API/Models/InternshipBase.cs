using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models
{
    public class InternshipBase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("base_name")]
        public string BaseName { get; set; }

        [BsonElement("base_address")]
        public string? BaseAddress { get; set; }
    }
}
