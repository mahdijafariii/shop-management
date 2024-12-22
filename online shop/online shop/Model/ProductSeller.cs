using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shop.Model;

public class ProductSeller
{
    [BsonRequired]
    public string SellerId { get; set; }

    [BsonRequired]
    public double Price { get; set; }

    [BsonRequired]
    public int Stock { get; set; }
    public ProductSeller(string sellerId, double price, int stock)
    {
        SellerId = sellerId;
        Price = price;
        Stock = stock;
    }
}

