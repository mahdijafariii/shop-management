using MongoDB.Bson;

namespace online_shop.DTO;

public record CreateCategoryDto(string Title , string Slug , ObjectId? Parent ,string Description , List<Filter> Filters ,  IFormFile? IconFile);