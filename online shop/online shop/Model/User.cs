using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class User
{
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonRequired]
    public string Phone { get; set; }
    
    [BsonRequired]
    public string Username { get; set; }
    
    [BsonElement("roles")] 
    public List<string> Roles { get; set; } = new List<string> { "USER" }; 

    [BsonElement("addresses")]
    public List<Address> Addresses { get; set; } = new List<Address>();

    [BsonDateTimeOptions]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonDateTimeOptions]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}