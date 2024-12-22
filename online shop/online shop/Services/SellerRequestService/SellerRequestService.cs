using MongoDB.Bson;
using Newtonsoft.Json;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories.ProductRepository;
using online_shop.Repositories.SellerRequestRepository;

namespace online_shop.Services;

public class SellerRequestService : ISellerRequestService
{
    private readonly ISellerRequestRepository _sellerRequestRepository;
    private readonly IProductRepository _productRepository;


    public SellerRequestService(ISellerRequestRepository sellerRequestRepository, IProductRepository productRepository)
    {
        _sellerRequestRepository = sellerRequestRepository;
        _productRepository = productRepository;
    }

    public async Task<string> CreateRequest(string sellerId, CreateSellerRequest request)
    {
        var product = await _productRepository.IsProductExist(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }

        var requestExistBefore = await _sellerRequestRepository.IsRequestExistAsync(sellerId, request.ProductId);
        if (requestExistBefore.Status == SellerRequestStatus.Pending.ToString())
        {
            throw new InvalidRequestException("You send request before please wait", 404);
        }

        var sellerRequest = new SellerRequest()
        {
            ProductId = request.ProductId,
            SellerId = sellerId,
            Stock = request.Stock,
            Price = request.Price,
        };
        var id = await _sellerRequestRepository.AddSellerRequestAsync(sellerRequest);
        return id;
    }

    public async Task DeleteSellerRequest(string requestId, string userId)
    {
        var note = await _sellerRequestRepository.GetSellerRequestAsync(requestId);
        if (note is null)
        {
            throw new NotFoundException("Note");
        }

        if (note.Id != userId)
        {
            throw new InvalidRequestException("you dont access to delete this note", 400);
        }

        if (note.Status != SellerRequestStatus.Pending.ToString())
        {
            throw new InvalidRequestException("The status of request is pending and you can not delete it !", 400);
        }
        
    }

    public async Task<(List<SellerRequest>, int totalCount)> GetAllRequest(string userId, int page = 1, int limit = 10, string status = "Pending")
    {
        Console.WriteLine($"status{status}");
        var result = await _sellerRequestRepository.GetAllRequestAsync(userId, page, limit,status);
        if (!result.Any() || result is null)
        {
            throw new NotFoundException("Seller request");
        }
        var totalCount = await _sellerRequestRepository.SellerRequestTotalCount();
        return (result, totalCount);
    }

    public async Task<Product> UpdateSellerRequest(UpdateSellerRequestDto requestDto)
    {
        if (requestDto.Status.ToLower() != "Approved" || requestDto.Status.ToLower() != "Rejected")
        {
            throw new InvalidRequestException("Status should be Approved or Rejected", 400);
        }

        if (requestDto.Status == "Rejected")
        {
            var check = await _sellerRequestRepository.UpdateSellerRequestAsync(requestDto);
            if (!check)
            {
                throw new InvalidRequestException("Error in update seller request", 400);
            }
        }
        else if (requestDto.Status == "Approved")
        {
            var sellerRequest = await _sellerRequestRepository.GetSellerRequestAsync(requestDto.RequestId);
            if (sellerRequest is null)
            {
                throw new NotFoundException("Seller Request");
            }

            var product = await _productRepository.GetProductAsync(sellerRequest.ProductId);
            
            if (product is null)
            {
                throw new NotFoundException("Product");
            }
            List<ProductSeller> sellers = JsonConvert.DeserializeObject<List<ProductSeller>>(product.Sellers.ToJson());
            var findSeller = sellers.Find(p => p.SellerId.ToString() == sellerRequest.SellerId);
            if (findSeller is not null)
            {
                requestDto.AdminComment = "you are the seller of this product";
                throw new InvalidRequestException("We have seller with this id",400);
            }
            sellers.Add(new ProductSeller(sellerRequest.SellerId,(decimal)sellerRequest.Price,sellerRequest.Stock));
            var updateRequest= await _sellerRequestRepository.UpdateSellerRequestAsync(requestDto);
            var updateProduct = await _sellerRequestRepository.UpdateProductSellersAsync(sellerRequest.ProductId, sellers);
            if (!updateProduct || !updateRequest)
            {
                throw new InvalidRequestException("Update failed", 400);
            }

            var updatedProduct = await _productRepository.GetProductAsync(sellerRequest.ProductId);
            return updatedProduct;
        }

        return null;
    }
    
}