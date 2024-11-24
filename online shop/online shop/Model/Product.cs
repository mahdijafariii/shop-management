using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Product
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonRequired]
    public string Name { get; set; }

    [BsonRequired]
    [BsonElement("slug")]
    public string Slug { get; set; }

    [BsonRequired]
    public string Description { get; set; }

    [BsonRequired]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId SubCategory { get; set; }

    [BsonRequired]
    public List<string> Images { get; set; }

    [BsonRequired]
    public List<Seller> Sellers { get; set; }

    [BsonRequired]
    public Dictionary<string, object> FilterValues { get; set; }

    [BsonRequired]
    public Dictionary<string, string> CustomFilters { get; set; }

    [BsonRequired]
    [BsonElement("shordIdentifier")]
    public string ShortIdentifier { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}