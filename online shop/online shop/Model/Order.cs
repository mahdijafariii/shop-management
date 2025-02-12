using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 
    public string UserId { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public ShippingAddress ShippingAddress { get; set; }

    public string PostTrackingCode { get; set; }

    public string Status { get; set; } = OrderStatus.PROCESSING.ToString();

    public string Authority { get; set; }
    [BsonElement("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    [BsonElement("updatedAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}
public enum OrderStatus
{
    PROCESSING,
    SHIPPED,
    DELIVERED
}