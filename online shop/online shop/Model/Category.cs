using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Category
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
    [JsonPropertyName("parent")]
    public ObjectId? ParentId { get; set; } 

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("icon")]
    public Icon? Icon { get; set; } 

    [BsonElement("filters")]
    public List<Filter> Filters { get; set; }
}

public class Icon
{
    [BsonElement("filename")]
    [Required(ErrorMessage = "Filename is required.")]
    public string Filename { get; set; }

    [BsonElement("path")]
    [Required(ErrorMessage = "Path is required.")]
    public string Path { get; set; }
}

