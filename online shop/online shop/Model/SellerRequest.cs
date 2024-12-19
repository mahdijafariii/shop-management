using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class SellerRequest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] 
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)] 
    public string SellerId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)] 
    public string ProductId { get; set; }

    public double Price { get; set; }

    public int Stock { get; set; }

    [BsonRepresentation(BsonType.String)] 
    public string Status { get; set; } = SellerRequestStatus.Pending.ToString();

    public string AdminComment { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)] 
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public enum SellerRequestStatus
{
    Pending,
    Accepted,
    Rejected
}