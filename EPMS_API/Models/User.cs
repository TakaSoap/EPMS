using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EPMS_API.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("user_type")]
    public string UserType { get; set; }

    [BsonElement("user_id_authing")]
    public string UserIdAuthing { get; set; }

    [BsonElement("base_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string baseId { get; set; }

    public User(string name, string phoneNumber, string email, string userType, string userIdAuthing, string baseId)
    {
        Id = "";
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        UserType = userType;
        UserIdAuthing = userIdAuthing;
        this.baseId = baseId;
    }

    public User()
    {
        Id = "";
        Name = "";
        PhoneNumber = "";
        Email = "";
        UserType = "";
        UserIdAuthing = "";
        baseId = "";
    }
}