namespace online_shop.DTO;

public record CreateProductDto(string Name,
    string Slug,
    string Description,
    string SubCategory,
    List<string> Sellers,
    Dictionary<string,object> FilterValues,
    Dictionary<string,string> CustomFilters
    , List<IFormFile> Files);