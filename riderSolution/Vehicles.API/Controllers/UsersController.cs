using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Requests.Users.Users.Commands;
using Vehicles.Application.Requests.Users.Users.Queries;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("addPostToFavorite/{userId}/{postId}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> AddPostToFavorite([FromRoute] string userId, [FromRoute] int postId)
    {
        var command = new AddPostToFavoriteList(userId, postId);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete("removePostFromFavorite/{userId}/{postId}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> RemovePostFromFavorite([FromRoute] string userId, [FromRoute] int postId)
    {
        var command = new RemovePostFromFavoriteList(userId, postId);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }

    [HttpGet("isPostFavorite/{userId}/{postId}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> IsPostFavorite([FromRoute] string userId, [FromRoute] int postId)
    {
        var command = new IsPostFavorite(userId, postId);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpGet("getUserSubscriptions/{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> GetUserSubscriptions([FromRoute] string id)
    {
        var command = new GetUserSubscriptions(id);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
}