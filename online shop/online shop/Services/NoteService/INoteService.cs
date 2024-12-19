using online_shop.DTO;
using online_shop.Model;

namespace online_shop.Services;

public interface INoteService
{
    Task<Note> AddNote(AddNote request, string userId);
    Task<Note> GetNote(string noteId, string userId);
    
}