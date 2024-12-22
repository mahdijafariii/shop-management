using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories.CommentRepository;

public class CommentRepository : ICommentRepository
{
    private readonly MongoDbContext _dbContext;

    public CommentRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment> AddNoteAsync(Comment note)
    {
        await _dbContext.Comment.InsertOneAsync(note);
        return note;
    }
}