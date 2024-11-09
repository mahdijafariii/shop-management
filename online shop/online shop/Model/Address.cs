using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Address
{
    [BsonId] 
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    
    [BsonRequired]
    public string Name { get; set; }

    [BsonRequired]
    public string PostalCode { get; set; }

    [BsonRequired]
    public double Lat { get; set; }

    [BsonRequired]
    public double Lng { get; set; }

    [BsonRequired]
    public string AddressLine { get; set; } 
    
    [BsonRequired]
    public int CityId { get; set; }
}