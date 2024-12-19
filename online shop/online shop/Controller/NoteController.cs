using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
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
}