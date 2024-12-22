using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class Comment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string ProductId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public int Rating { get; set; }

    public string Content { get; set; }

    public List<ReplyComment> Replies { get; set; }

    [BsonElement("createdAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; }
}