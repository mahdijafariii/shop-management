using online_shop.Model;

namespace online_shop.Repositories.NoteRepository;

public interface INoteRepository
{
    Task<Note> AddNoteAsync(Note note);
    Task<bool> NoteExistAsync(string userId , string productId);
    Task DeleteNoteAsync();
    Task GetNoteAsync();
}