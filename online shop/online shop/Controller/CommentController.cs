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

    [HttpGet("get-product-comments")]
    public async Task<IActionResult> GetCommentOfProductAsync([FromQuery] string productId)
    {
        var result = await _commentService.GetProductComments(productId);
        return Ok(result);
    } 
    
    [HttpDelete("delete-reply-comment")]
    public async Task<IActionResult> DeleteReplyCommentAsync([FromQuery] string replyCommentId, string commentId)
    {
        await _commentService.DeleteReplyComment(replyCommentId,commentId);
        return Ok("reply comment deleted successfully");
    }   
    
    [HttpDelete("delete-comment")]
    public async Task<IActionResult> DeleteCommentAsync([FromQuery] string commentId)
    {
        await _commentService.DeleteComment(commentId);
        return Ok("comment deleted successfully");
    }   
    
    [HttpPatch("update-comment")]
    public async Task<IActionResult> UpdateCommentAsync([FromQuery] UpdateComment request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        await _commentService.UpdateComment(request,userId);
        return Ok("Comment updated successfully");
    } 
    [HttpPatch("update-reply-comment")]
    public async Task<IActionResult> UpdateReplyCommentAsync([FromQuery] UpdateReplyComment request)
    {
        var user = User;
        var userId = user.FindFirstValue("userId");
        await _commentService.UpdateReplyComment(request,userId);
        return Ok("Reply Comment updated successfully");
    } 
}