namespace online_shop.DTO;

public record CreateProductDto(string Name,
    string Slug,
    string Description,
    string SubCategory,
    List<string> Sellers,
    string FilterValues,
    string CustomFilters
    , List<IFormFile> Files);