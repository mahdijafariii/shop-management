namespace online_shop.DTO;

public record AddToCartDto(string ProductId ,string SellerId ,int Quantity ,double PriceAtTimeOfAdding);