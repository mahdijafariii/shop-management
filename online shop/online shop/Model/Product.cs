using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace online_shop.Model;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]    
    public string Id { get; set; }

    [BsonRequired]
    public string Name { get; set; }

    [BsonRequired]
    [BsonElement("slug")]
    public string Slug { get; set; }

    [BsonRequired]
    public string Description { get; set; }

    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public string SubCategory { get; set; }

    [BsonRequired]
    public List<string> Images { get; set; }

    [BsonRequired]
    public List<string> Sellers { get; set; }

    [BsonRequired]
    public Dictionary<string, object> FilterValues { get; set; }

    [BsonRequired]
    public Dictionary<string, string> CustomFilters { get; set; }

    [BsonRequired]
    [BsonElement("shortIdentifier")]
    public string ShortIdentifier { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}