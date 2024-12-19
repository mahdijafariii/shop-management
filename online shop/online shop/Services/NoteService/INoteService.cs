using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface INoteService
{
    Task<Note> AddNote(AddNote request, string userId);
    Task<Note> GetNote(string noteId, string userId);
    Task<(List<NoteWithProduct>, int totalCount)> GetAllNotes(string userId, int page , int limit);
    Task<bool> DeleteNote(string userId, string noteId);

}