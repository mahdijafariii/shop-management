namespace online_shop.DTO;

public record UpdateProduct(string ProductId,string? Name,
    string? Slug,
    string? Description,
    string? SubCategory,
    string? FilterValues,
    string? CustomFilters
    , List<IFormFile>? Files);

