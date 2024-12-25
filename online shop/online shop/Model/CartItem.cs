using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class CartItem
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; }
    
    [BsonRepresentation(BsonType.ObjectId)]
    public string SellerId { get; set; }
    
    public int Quantity { get; set; }

    [BsonElement("priceAtTimeOfAdding")]
    public decimal PriceAtTimeOfAdding { get; set; }
}