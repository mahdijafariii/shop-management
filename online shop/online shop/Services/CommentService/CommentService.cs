using MongoDB.Bson;
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
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
        var noteResult = await _commentRepository.AddNoteAsync(comment);
        return noteResult;
    }

    public async Task<bool> DeleteComment(string commentId)
    {
        var check = await _commentRepository.DeleteCommentAsync(commentId);
        if (!check)
        {
            throw new InvalidRequestException("request was not successful", 400);
        }
        return check;
    }

    public async Task<List<Comment>> GetProductComments(string productId)
    {
        var result = await _commentRepository.GetProductCommentsAsync(productId);
        if (!result.Any())
        {
            throw new NotFoundException("Comment for product");
        }
        return result;
    }

    public async Task<ReplyComment> AddReplyComment(AddReplyCommentDto request, string userId)
    {
        ReplyComment replyComment = new ReplyComment()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            UserId = userId
        };
        var result = await _commentRepository.AddReplyCommentAsync(replyComment,request.CommentId);
        if (result is null)
        {
            throw new NotFoundException("Comment");
        }
        return replyComment;
    }

    public async Task DeleteReplyComment(string replyCommentId, string commentId)
    {
        var check = await _commentRepository.CheckReplyCommentExistAsync(replyCommentId, commentId);
        if (!check)
        {
            throw new NotFoundException("Comment or ReplyComment");
        }

        var checkDeleted = await _commentRepository.DeleteReplyCommentAsync(replyCommentId, commentId);
        if (!checkDeleted)
        {
            throw new InvalidRequestException("The reply comment was not deleted successfully", 400);
        }
    }

    public async Task UpdateComment(UpdateComment request,string userId)
    {
        var result = await _commentRepository.UpdateComment(request, userId);
        if (!result)
        {
            throw new InvalidRequestException("Update was not successful", 400);
        }
    }

    public async Task UpdateReplyComment(UpdateReplyComment request,string userId)
    {
        var result = await _commentRepository.UpdateReplyComment(request, userId);
        if (!result)
        {
            throw new InvalidRequestException("Update was not successful", 400);
        }
    }
}