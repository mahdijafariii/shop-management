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
        var result = await _noteService.AddNote(request);
        return Ok(new
        {
            Massage = "Added successfully",
            note = result
        });
    }    
}