using MongoDB.Driver;
using online_shop.Data;
using online_shop.DTO;
using online_shop.Exception;
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

    public async Task<List<Comment>> GetProductCommentsAsync(string productId)
    {
        var filter = Builders<Comment>.Filter.Eq(c => c.ProductId, productId);
        var result = await _dbContext.Comment.FindAsync(filter);
        return result.ToList();
    }

    public async Task<ReplyComment> AddReplyCommentAsync(ReplyComment replyComment, string commentId)
    {
        var filter = Builders<Comment>.Filter.Eq(c => c.Id, commentId);
        var result = await _dbContext.Comment.Find(filter).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }
        if (result.Replies == null)
        {
            result.Replies = new List<ReplyComment>();
        }
        result.Replies.Add(replyComment);
        var update = Builders<Comment>.Update.Set(c => c.Replies, result.Replies);
        await _dbContext.Comment.UpdateOneAsync(filter, update);
        return replyComment;
    }

    public async Task<bool> CheckReplyCommentExistAsync(string replyCommentId, string commentId)
    {
        var result = await _dbContext.Comment.Find(p => p.Id == commentId)
            .FirstOrDefaultAsync();
        if (result is null)
        {
            return false;
        }
        var check = result.Replies.Any(x => x.Id == replyCommentId);
        return check;
    }

    public async Task<bool> DeleteReplyCommentAsync(string replyCommentId, string commentId)
    {
        var filter = Builders<Comment>.Filter.Eq(c => c.Id, commentId);
        var result = await _dbContext.Comment.Find(filter).FirstOrDefaultAsync();
        if (result.Replies == null)
        {
            return false;
        }
        var replyComment = result.Replies.FirstOrDefault(replyComment => replyComment.Id == replyCommentId);
        result.Replies.Remove(replyComment);
        var update = Builders<Comment>.Update.Set(c => c.Replies, result.Replies);
        var updateResult = await _dbContext.Comment.UpdateOneAsync(filter, update);
        return updateResult.ModifiedCount > 0;
    }

    public async Task<bool> UpdateReplyComment(UpdateReplyComment replyComment, string userId)
    {
        var filter = Builders<Comment>.Filter.Eq(c => c.Id, replyComment.CommentId);
        var result = await _dbContext.Comment.Find(filter).FirstOrDefaultAsync();
        if (result is null)
        {
            throw new NotFoundException("Comment");
        }
        if (result.Replies == null)
        {
            throw new NotFoundException("Reply Comment");
        }
        var found = result.Replies.FirstOrDefault(x => x.Id == replyComment.ReplyCommentId);
        if (found == null)
        {
            throw new NotFoundException("Reply Comment");
        }
        found.Content = replyComment.NewContent;
        var update = Builders<Comment>.Update.Set(c => c.Replies, result.Replies);
        var updateResult = await _dbContext.Comment.UpdateOneAsync(filter, update);
        return updateResult.ModifiedCount > 0;
    }

    public async Task<bool> UpdateComment(UpdateComment updateComment,string userId)
    {
        var filter = Builders<Comment>.Filter.And(
            Builders<Comment>.Filter.Eq(s => s.Id, updateComment.CommentId),
            Builders<Comment>.Filter.Eq(s => s.UserId, userId)
        );
        var updateDefinition = new List<UpdateDefinition<Comment>>();
        if (updateComment.Rating != null)
        {
            updateDefinition.Add(Builders<Comment>.Update.Set(s => s.Content, updateComment.NewContent));
        }
        if (updateDefinition.Any())
        {
            var combinedUpdate = Builders<Comment>.Update.Combine(updateDefinition);
            var result = await _dbContext.Comment.UpdateOneAsync(filter, combinedUpdate);
            return result.ModifiedCount > 0;
        }
        return false;
    }
}