using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Exception;
using online_shop.Services;

namespace online_shop.Controller;


[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpPost("add-note")]
    public async Task<IActionResult> AddNoteAsync([FromBody] AddNote request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _noteService.AddNote(request,userId);
        return Ok(new
        {
            Massage = "Added successfully",
            note = result
        });
    }    
    
    [HttpGet("get-note")]
    public async Task<IActionResult> GetNoteAsync([FromQuery] string noteId)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _noteService.GetNote(noteId,userId);
        return Ok(result);
    }    
    [HttpGet("get-all-notes")]
    public async Task<IActionResult> GetAllNotesAsync([FromQuery] PaginationInputDto request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _noteService.GetAllNotes(userId,request.Page,request.Limit);
        return Ok(new
        {
            result.Item1,
            total_count = result.totalCount,
            page = request.Page,
            limit = request.Limit
        });
    }    
    
    [HttpDelete("delete-note")]
    public async Task<IActionResult> DeleteNoteAsync([FromQuery] string noteId)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        await _noteService.DeleteNote(userId, noteId);
        return Ok("note deleted successfully");
    }   
}