using online_shop.Model;

namespace online_shop.Repositories.NoteRepository;

public interface INoteRepository
{
    Task<Note> AddNoteAsync(Note note);
    Task<bool> NoteExistAsync(string userId , string productId);
    Task<Note> GetNoteAsync(string noteId, string userId);
    Task<List<Note>> GetAllNote(string userId, int page, int limit);
    Task<bool> DeleteNoteAsync(string noteId);
    Task<int> NoteTotalCount();
    Task<bool> UpdateNoteAsync(string userId, string noteId, string content);

}