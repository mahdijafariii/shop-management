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

    public async Task<Note> AddNote(AddNote request, string userId)
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
            throw new InvalidRequestException("We dose not have note with this id or we can not send it to you",400);
        }

        var product = await _productRepository.IsProductExist(note.ProductId);
        if (product is null)
        {
            throw new NotFoundException("Product");
        }
        return note;
    }
}