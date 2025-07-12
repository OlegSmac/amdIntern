using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Services;
using Vehicles.Application.Users.Users.Commands;
using Vehicles.Application.Users.Users.Queries;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityService _identityService;

    public UsersController(IMediator mediator, UserManager<ApplicationUser> userManager, IdentityService identityService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _identityService = identityService;
    }

    [HttpPost("addPostToFavorite/{userId}/{postId}")]
    public async Task<IActionResult> AddPostToFavorite([FromRoute] string userId, [FromRoute] int postId)
    {
        var command = new AddPostToFavoriteList(userId, postId);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpDelete("removePostFromFavorite/{userId}/{postId}")]
    public async Task<IActionResult> RemovePostFromFavorite([FromRoute] string userId, [FromRoute] int postId)
    {
        var command = new RemovePostFromFavoriteList(userId, postId);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }

    [HttpGet("isPostFavorite/{userId}/{postId}")]
    public async Task<IActionResult> IsPostFavorite([FromRoute] string userId, [FromRoute] int postId)
    {
        var command = new IsPostFavorite(userId, postId);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpGet("getUserSubscriptions/{id}")]
    public async Task<IActionResult> GetUserSubscriptions([FromRoute] string id)
    {
        var command = new GetUserSubscriptions(id);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
}