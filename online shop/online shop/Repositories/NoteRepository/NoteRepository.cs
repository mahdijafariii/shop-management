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
    public async Task<bool> NoteExistAsync(string userId , string productId)
    {
        var result = await _dbContext.Note.Find(p => p.ProductId == productId && p.UserId == userId).FirstOrDefaultAsync();
        return result != null;
    }

    public Task DeleteNoteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Note> GetNoteAsync(string noteId, string userId)
    {
        var result = await _dbContext.Note.Find(p => p.Id == noteId && p.UserId == userId).FirstOrDefaultAsync();
        if(result is null)
        {
            return null;
        }
        return result;
    }
}