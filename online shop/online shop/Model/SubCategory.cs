using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class SubCategory
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("title")]
    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [BsonElement("slug")]
    [Required(ErrorMessage = "Slug is required.")]
    public string Slug { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("parent")]
    [Required(ErrorMessage = "ParentId is required.")]
    public string ParentId { get; set; } 

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("filters")]
    public List<Filter> Filters { get; set; }
}

