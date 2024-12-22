using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories.CommentRepository;
using online_shop.Repositories.ProductRepository;

namespace online_shop.Services;

public class CommentService : ICommentService
{
    private readonly IProductRepository _productRepository;
    private readonly ICommentRepository _commentRepository;

    public CommentService(IProductRepository productRepository, ICommentRepository commentRepository)
    {
        _productRepository = productRepository;
        _commentRepository = commentRepository;
    }

    public async Task<Comment> AddComment(AddCommentDto request, string userId)
    {
        var result = await _commentRepository.CommentExistAsync(userId, request.ProductId);
        if (result)
        {
            throw new InvalidRequestException("Comment exist before for this product!", 400);
        }

        var product = await _productRepository.IsProductExist(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }

        var comment = new Comment
        {
            UserId = userId,
            Content = request.Content,
            ProductId = request.ProductId,
            Rating = request.Rating
        };
        var noteResult = await _commentRepository.AddNoteAsync(comment);
        return noteResult;
    }
}