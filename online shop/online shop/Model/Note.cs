using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Note
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 

    [BsonRequired]
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("productId")]
    public string ProductId { get; set; }

    [BsonElement("content")]
    [BsonRequired]
    public string Content { get; set; }
}