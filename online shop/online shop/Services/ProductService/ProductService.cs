using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories.ProductRepository;

namespace online_shop.Services.ProductService;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task CreateProduct(CreateProductDto model)
    {
        if (!ObjectId.TryParse(model.SubCategory, out _))
            throw new InvalidRequestException("SubCategory ID is not correct!", 404);


        var images = new List<string>();
        foreach (var file in model.Files)
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

        string shortIdentifier;
        do
        {
            shortIdentifier = Guid.NewGuid().ToString("N").Substring(0, 6);
        } while (await _productRepository.IsShortIdentifierExistsAsync(shortIdentifier));

        var filtersFromJson = SetFiltersFromJson(model.FilterValues);
        var customFiltersFromJson = SetCustomFiltersFromJson(model.CustomFilters);


        var newProduct = new Product
        {
            Name = model.Name,
            Slug = model.Slug,
            Description = model.Description,
            SubCategoryId = model.SubCategory,
            Images = images,
            Sellers = model.Sellers,
            FilterValues = filtersFromJson,
            CustomFilters = customFiltersFromJson,
            ShortIdentifier = shortIdentifier,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        await _productRepository.AddProductAsync(newProduct);
    }

    public async Task DeletesProduct(string request)
    {
        var product = await _productRepository.DeleteProductAsync(request);
        if (product is null)
        {
            throw new OperationFailedException();
        }

        if (product.Images != null && product.Images.Any())
        {
            foreach (var imagePath in product.Images)
            {
                try
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"can`t delete image {ex}");
                }
            }
        }
    }

    public async Task<Product>   GetProduct(string productId)
    {
        var product = await _productRepository.GetProductAsync(productId);
        if (product is null)
        {
            throw new OperationFailedException();
        }

        return product;
    }

    public async Task<Product> GetProductWithIdentifier(string shortIdentifier)
    {
        var product = await _productRepository.GetProductWithIdentifier(shortIdentifier);
        if (product is null)
        {
            throw new NotFoundException("product");
        }

        return product;
    }

    public async Task UpdateProduct(UpdateProduct request)
    {
        var product = await _productRepository.GetProductAsync(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("product");
        }

        var check = await _productRepository.UpdateProductAsync(request);
        if (!check)
        {
            throw new InvalidRequestException("update was not successful",400);
        }
    }

    private static readonly string[] SupportedFormats =
    {
        "image/jpeg",
        "image/png",
        "image/svg+xml",
        "image/webp",
        "image/gif"
    };

    public Dictionary<string, object> SetFiltersFromJson(string json)
    {
        Dictionary<string, object> filterValues = new Dictionary<string, object>();
        var jobject = JObject.Parse(json);
        var dictionary = jobject.ToObject<Dictionary<string, object>>();

        return dictionary ?? filterValues;
    }

    public Dictionary<string, string> SetCustomFiltersFromJson(string json)
    {
        Dictionary<string, string> customFilters = new Dictionary<string, string>();
        var jobject = JObject.Parse(json);
        var dictionary = jobject.ToObject<Dictionary<string, string>>();
        return dictionary ?? customFilters;
    }
}