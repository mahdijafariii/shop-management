using online_shop.DTO;
using online_shop.Model;
using online_shop.Repositories.NoteRepository;

namespace online_shop.Services;

public class NoteService : INoteService
{
    private INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<Note> AddNote(AddNote request)
    {
        var note = new Note
        {
            UserId = request.UserId,
            Content = request.Content,
            ProductId = request.ProductId
        };
        var noteResult = await _noteRepository.AddNoteAsync(note);
        return noteResult;
    }
}