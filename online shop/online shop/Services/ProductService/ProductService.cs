using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;

namespace online_shop.Services.ProductService;

public class ProductService
{
    
    public async Task CreateProductAsync(CreateProductDto model)
    {
        if (!Guid.TryParse(model.SubCategory, out _))
            throw new InvalidRequestException("SubCategory ID is not correct!",404);


        var images = new List<string>();
        foreach (var file in model.Files)
        {
            if (!SupportedFormats.Contains(file.ContentType))
                throw new InvalidRequestException("Unsupported image format!",404);

            var filePath = Path.Combine("Uploads", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            images.Add(file.FileName);
        }

        string shortIdentifier;
        do
        {
            shortIdentifier = Guid.NewGuid().ToString("N").Substring(0, 6);
        } while (await _productRepository.IsShortIdentifierExistsAsync(shortIdentifier));

        var newProduct = new Product
        {
            Name = model.Name,
            Slug = model.Slug,
            Description = model.Description,
            SubCategory = model.SubCategory,
            Images = images,
            Sellers = model.Sellers,
            FilterValues = model.FilterValues,
            CustomFilters = model.CustomFilters,
            ShortIdentifier = shortIdentifier
        };

        await _productRepository.AddProductAsync(newProduct);

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