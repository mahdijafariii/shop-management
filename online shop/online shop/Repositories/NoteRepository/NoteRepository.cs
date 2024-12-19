using MongoDB.Driver;
using online_shop.Data;
using online_shop.Model;

namespace online_shop.Repositories.NoteRepository;

public class NoteRepository : INoteRepository
{
    private readonly MongoDbContext _dbContext;

    public NoteRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Note> AddNoteAsync(Note note)
    {
        await _dbContext.Note.InsertOneAsync(note);
        return note;
    }

    public async Task<bool> NoteExistAsync(string userId, string productId)
    {
        var result = await _dbContext.Note.Find(p => p.ProductId == productId && p.UserId == userId)
            .FirstOrDefaultAsync();
        return result != null;
    }

    public Task DeleteNoteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Note> GetNoteAsync(string noteId, string userId)
    {
        var result = await _dbContext.Note.Find(p => p.Id == noteId && p.UserId == userId).FirstOrDefaultAsync();
        if (result is null)
        {
            return null;
        }

        return result;
    }

    public async Task<List<Note>> GetAllNote(string userId, int page, int limit)
    {
        var skip = (page - 1) * limit;

        var result = await _dbContext.Note.Find(p => p.UserId == userId).Skip(skip)
            .Limit(limit).ToListAsync();
        if (result is null)
        {
            return null;
        }
        return result;
    }
    
    public async Task<bool> DeleteNoteAsync(string noteId)
    {
        var filter = Builders<Note>.Filter.Eq(c => c.Id, noteId);
        var result = await _dbContext.Note.FindOneAndDeleteAsync(filter);
        return result != null;
    }
    
    public async Task<int> NoteTotalCount()
    {
        var totalCount = await _dbContext.Users.CountDocumentsAsync(FilterDefinition<User>.Empty);
        return (int)totalCount;
    }
    

}