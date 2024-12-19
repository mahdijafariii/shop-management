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

    public Task DeleteNoteAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetNoteAsync()
    {
        throw new NotImplementedException();
    }
}