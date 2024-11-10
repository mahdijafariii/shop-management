using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Seller
{
    [BsonId]
    public ObjectId Id { get; set; } 

    [BsonRequired]
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("contactDetails")]
    public ContactDetails ContactDetails { get; set; }

    [BsonElement("cityId")]
    [BsonRequired]
    public int CityId { get; set; }

    [BsonElement("user")]
    [BsonRequired]
    public ObjectId User { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}
public class ContactDetails
{
    [BsonElement("phone")]
    [BsonRequired]
    public string Phone { get; set; }
}