using MongoDB.Driver;
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

    public async Task<bool> CommentExistAsync(string userId, string productId)
    {
        var result = await _dbContext.Comment.Find(p => p.ProductId == productId && p.UserId == userId)
            .FirstOrDefaultAsync();
        return result != null;
    }
    
    public async Task<bool> DeleteCommentAsync(string commentId)
    {
        var filter = Builders<Comment>.Filter.Eq(c => c.Id, commentId);
        var result = await _dbContext.Comment.FindOneAndDeleteAsync(filter);
        return result != null;
    }
}