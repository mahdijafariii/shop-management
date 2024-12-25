using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Cart
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public List<CartItem> Items { get; set; } = new List<CartItem>();

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
}