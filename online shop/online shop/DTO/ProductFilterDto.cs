namespace online_shop.DTO;

public record ProductFilterDto(string? Name, string? SubCategory, double? MinPrice, double? MaxPrice, string? SellerId,
    int Page = 1, int Limit = 10);