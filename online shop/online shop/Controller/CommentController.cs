using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using online_shop.DTO;
using online_shop.Services;

namespace online_shop.Controller;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("add-comment")]
    public async Task<IActionResult> AddCommentAsync([FromBody] AddCommentDto request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _commentService.AddComment(request,userId);
        return Ok(new
        {
            Massage = "Added successfully",
            Comment = result
        });
    }
    
    [HttpDelete("delete-comment")]
    public async Task<IActionResult> DeleteCommentAsync([FromQuery] string commentId)
    {
        await _commentService.DeleteComment(commentId);
        return Ok("comment deleted successfully");
    }   
    
    [HttpGet("get-product-comments")]
    public async Task<IActionResult> GetCommentOfProductAsync([FromQuery] string productId)
    {
        var result = await _commentService.GetProductComments(productId);
        return Ok(result);
    } 
    [HttpPost("add-reply-comment")]
    public async Task<IActionResult> AddReplyCommentAsync([FromBody] AddReplyCommentDto request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        var result = await _commentService.AddReplyComment(request,userId);
        return Ok(new
        {
            Massage = "Added successfully",
            ReplyComment = result
        });
    }
    [HttpDelete("delete-reply-comment")]
    public async Task<IActionResult> DeleteReplyCommentAsync([FromQuery] string replyCommentId, string commentId)
    {
        await _commentService.DeleteReplyComment(replyCommentId,commentId);
        return Ok("reply comment deleted successfully");
    }   
    
}