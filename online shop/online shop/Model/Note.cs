using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Note
{
    [BsonId]
    public string Id { get; set; } 

    [BsonRequired]
    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("productId")]
    public ContactDetails ProductId { get; set; }

    [BsonElement("content")]
    [BsonRequired]
    public int Content { get; set; }
}