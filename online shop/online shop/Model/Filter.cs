using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

public class Filter
{
    [BsonElement("name")]
    [Required(ErrorMessage = "Filter name is required.")]
    public string Name { get; set; }

    [BsonElement("slug")]
    [Required(ErrorMessage = "Filter slug is required.")]
    public string Slug { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("type")]
    [Required(ErrorMessage = "Filter type is required.")]
    [EnumDataType(typeof(FilterType))]
    public FilterType Type { get; set; }

    [BsonElement("options")]
    public List<string> Options { get; set; }

    [BsonElement("min")]
    public int? Min { get; set; }

    [BsonElement("max")]
    public int? Max { get; set; }
}