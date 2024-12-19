using online_shop.Model;

namespace online_shop.Repositories.NoteRepository;

public interface INoteRepository
{
    Task<Note> AddNoteAsync(Note note);
    Task DeleteNoteAsync();
    Task GetNoteAsync();
}