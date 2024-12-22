using online_shop.DTO;
using online_shop.Exception;
using online_shop.Model;
using online_shop.Repositories.NoteRepository;
using online_shop.Repositories.ProductRepository;

namespace online_shop.Services;

public class NoteService : INoteService
{
    private INoteRepository _noteRepository;
    private IProductRepository _productRepository;


    public NoteService(INoteRepository noteRepository, IProductRepository productRepository)
    {
        _noteRepository = noteRepository;
        _productRepository = productRepository;
    }

    public async Task<Note> AddNote(AddNoteDto request, string userId)
    {
        var result = await _noteRepository.NoteExistAsync(userId, request.ProductId);
        if (result)
        {
            throw new InvalidRequestException("Note exist before for this product!", 400);
        }

        var product = await _productRepository.IsProductExist(request.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }

        var note = new Note
        {
            UserId = userId,
            Content = request.Content,
            ProductId = request.ProductId
        };
        var noteResult = await _noteRepository.AddNoteAsync(note);
        return noteResult;
    }


    public async Task<Note> GetNote(string noteId, string userId)
    {
        var note = await _noteRepository.GetNoteAsync(noteId, userId);
        if (note is null)
        {
            throw new InvalidRequestException("We dose not have note with this id or we can not send it to you", 400);
        }

        var product = await _productRepository.IsProductExist(note.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }

        return note;
    }

    public async Task<(List<NoteWithProduct>, int totalCount)> GetAllNotes(string userId, int page = 1, int limit = 10)
    {
        var notes = await _noteRepository.GetAllNote(userId, page, limit);
        if (!notes.Any())
        {
            throw new NotFoundException("Note");
        }

        var allNotes = new List<NoteWithProduct>();
        foreach (var note in notes)
        {
            var check = await _productRepository.IsProductExist(note.ProductId);
            if (check != null)
            {
                var noteWithProduct = new NoteWithProduct(check, note.Content);
                allNotes.Add(noteWithProduct);
            }
            else
            {
                await _noteRepository.DeleteNoteAsync(note.Id);
            }
        }

        var totalCount = await _noteRepository.NoteTotalCount();
        return (allNotes, totalCount);
    }

    public async Task<bool> DeleteNote(string userId, string noteId)
    {
        var result = await _noteRepository.GetNoteAsync(noteId, userId);
        if (result is null)
        {
            throw new InvalidRequestException("You can not delete this note", 400);
        }

        var check = await _noteRepository.DeleteNoteAsync(noteId);
        if (!check)
        {
            throw new InvalidRequestException("request was not successful", 400);
        }

        return check;
    }

    public async Task<bool> UpdateNote(string userId, string noteId, string content)
    {
        var result = await _noteRepository.UpdateNoteAsync(userId,noteId,content);
        if (!result)
        {
            throw new InvalidRequestException("Note dose not updated successfully", 400);
        }

        return true;
    }
}