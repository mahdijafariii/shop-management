using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface ICommentService
{
    Task<Comment> AddComment(AddCommentDto request, string userId);
    Task<bool> DeleteComment(string commentId);
    Task<List<Comment>> GetProductComments(string productId);
    Task<ReplyComment> AddReplyComment(AddReplyCommentDto request, string userId);
    Task DeleteReplyComment(string replyCommentId, string commentId);
    
}