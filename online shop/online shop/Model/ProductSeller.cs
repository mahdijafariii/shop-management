using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class ProductSeller
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonRequired]
    public ObjectId SellerId { get; set; }

    [BsonRequired]
    public decimal Price { get; set; }

    [BsonRequired]
    public int Stock { get; set; }
}