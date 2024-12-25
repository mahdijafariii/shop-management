using online_shop.Model;

namespace online_shop.Repositories.CommentRepository;

public interface ICommentRepository
{
    Task<Comment> AddNoteAsync(Comment note);
    Task<bool> CommentExistAsync(string userId , string productId);
    Task<bool> DeleteCommentAsync(string commentId);
    Task<List<Comment>> GetProductCommentsAsync(string productId);
    Task<ReplyComment> AddReplyCommentAsync(ReplyComment replyComment, string commentId);
    Task<bool> CheckReplyCommentExistAsync(string replyCommentId, string commentId);
    Task<bool> DeleteReplyCommentAsync(string replyCommentId, string commentId);


}