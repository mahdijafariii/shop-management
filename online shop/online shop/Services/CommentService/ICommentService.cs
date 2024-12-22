using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface ICommentService
{
    Task<Comment> AddComment(AddCommentDto request, string userId);

}