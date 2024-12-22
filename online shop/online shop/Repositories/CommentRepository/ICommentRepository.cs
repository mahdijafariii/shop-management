using online_shop.Model;

namespace online_shop.Repositories.CommentRepository;

public interface ICommentRepository
{
    Task<Comment> AddNoteAsync(Comment note);
}