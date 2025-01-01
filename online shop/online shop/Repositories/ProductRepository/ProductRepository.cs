using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using online_shop.Data;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;

namespace online_shop.Repositories.ProductRepository;

public class ProductRepository : IProductRepository
{
    private readonly MongoDbContext _dbContext;
    

    public ProductRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> IsShortIdentifierExistsAsync(string id)
    {
        var filter = Builders<Seller>.Filter.Eq(c => c.Id, id);
        var product = await _dbContext.Sellers.Find(filter).FirstOrDefaultAsync();
        return product != null;
    }
    public async Task<Product> DeleteProductAsync(string productId)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId);
        var product = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();

        if (product != null)
        {
            var result = await _dbContext.Product.DeleteOneAsync(filter);
            if (result.DeletedCount > 0)
            {
                return product; 
            }
        }
    
        return null;
    }

    public async Task<Product> GetProductAsync(string productId)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId);
        var product = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();
        if (product != null)
        {
            var subCategory = await _dbContext.SubCategories
                .Find(s => s.Id == product.SubCategoryId)
                .FirstOrDefaultAsync();
            product.SubCategory = subCategory;
        }
        return product != null ? product : null;
    }

    public async Task<Product> IsProductExist(string productId)
    {
        var result = await _dbContext.Product.Find(p => p.Id == productId).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }

        return result;
    }

    public async Task<Product> GetProductWithIdentifier(string shortIdentifier)
    {
        var result = await _dbContext.Product.Find(p => p.ShortIdentifier == shortIdentifier).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }

        return result;
    }

    public async Task<bool> UpdateProductAsync(UpdateProduct request)
    {
        var filter = Builders<Product>.Filter.Eq(s => s.Id, request.ProductId);

        var updateDefinition = new List<UpdateDefinition<Product>>();

        if (!string.IsNullOrEmpty(request.Name))
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.Name, request.Name));

        if (!string.IsNullOrEmpty(request.SubCategory))
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.SubCategoryId,request.SubCategory));

        if (!string.IsNullOrEmpty(request.Description))
        {
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.Description, request.Description));
        }
        if (!string.IsNullOrEmpty(request.CustomFilters))
        {
            var customFilter = SetCustomFiltersFromJson(request.CustomFilters);
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.CustomFilters, customFilter));
        }
        if (!string.IsNullOrEmpty(request.FilterValues))
        {
            var mainFilter = SetFiltersFromJson(request.FilterValues);
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.FilterValues,mainFilter));
        }
        if (!string.IsNullOrEmpty(request.Slug))
        {
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.Slug, request.Slug));
        }
        if (request.Files != null && request.Files.Any())
        {
            var images = new List<string>();
            foreach (var file in request.Files)
            {
                if (!SupportedFormats.Contains(file.ContentType))
                    throw new InvalidRequestException("Unsupported image format!", 404);

                string uuid = Guid.NewGuid().ToString();
                var fileName = $"{uuid}{Path.GetExtension(file.FileName)}";
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "../../online shop/online shop/Public/images/Product");
                var filePath = Path.Combine(directoryPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                images.Add(filePath);
            }
            updateDefinition.Add(Builders<Product>.Update.Set(s => s.Slug, request.Slug));
        }
        if (updateDefinition.Any())
        {
            var combinedUpdate = Builders<Product>.Update.Combine(updateDefinition);
            var result = await _dbContext.Product.UpdateOneAsync(filter, combinedUpdate);
            return result.ModifiedCount > 0;
        }

        return false;
    }

    public async Task<ProductSeller> IsProductSeller(string productId, string sellerId)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId);
        var product = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();
        var sellersJson = string.Join(",", product.Sellers); 
        List<ProductSeller> sellers = JsonConvert.DeserializeObject<List<ProductSeller>>("[" + sellersJson + "]");
        var check = sellers.FirstOrDefault(x => x.SellerId == sellerId);
        if (check is null)
        {
            return null;
        }
        return check;
    }
    
    public async Task<bool> HasSufficientStock(string productId, string sellerId, int count)
    {
        var filter = Builders<Product>.Filter.Eq(c => c.Id, productId);
        var product = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();
        var sellersJson = string.Join(",", product.Sellers); 
        List<ProductSeller> sellers = JsonConvert.DeserializeObject<List<ProductSeller>>("[" + sellersJson + "]");
        var seller = sellers.FirstOrDefault(x => x.SellerId == sellerId);
        var check = seller.Stock >= count;
        return check;
    }

    public async Task DecreaseStock(List<CartItem> cartItems)
    {
        foreach (var product in cartItems)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, product.ProductId);
            var result = await _dbContext.Product.Find(filter).FirstOrDefaultAsync();
            var sellersJson = string.Join(",", result.Sellers); 
            List<ProductSeller> sellers = JsonConvert.DeserializeObject<List<ProductSeller>>("[" + sellersJson + "]");
            var seller = sellers.FirstOrDefault(x => x.SellerId == product.SellerId);
            seller.Stock -= product.Quantity;
            var sellersInJson = sellers.Select(seller => JsonConvert.SerializeObject(seller)).ToList();
            var update = Builders<Product>.Update.Set<List<string>>(p => p.Sellers, sellersInJson);
            await _dbContext.Product.UpdateOneAsync(filter, update);
        }
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _dbContext.Product.InsertOneAsync(product);
        return product;
    }
    
    public Dictionary<string, string> SetCustomFiltersFromJson(string json)
    {
        Dictionary<string, string> customFilters = new Dictionary<string, string>();
        var jobject = JObject.Parse(json);
        var dictionary = jobject.ToObject<Dictionary<string, string>>();
        return dictionary ?? customFilters;
    }
    
    public Dictionary<string, object> SetFiltersFromJson(string json)
    {
        Dictionary<string, object> filterValues = new Dictionary<string, object>();
        var jobject = JObject.Parse(json);
        var dictionary = jobject.ToObject<Dictionary<string, object>>();

        return dictionary ?? filterValues;
    }
    private static readonly string[] SupportedFormats =
    {
        "image/jpeg",
        "image/png",
        "image/svg+xml",
        "image/webp",
        "image/gif"
    };
}