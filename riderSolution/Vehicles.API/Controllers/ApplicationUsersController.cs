using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.Requests.Users;
using Vehicles.API.Services;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/application-users")]
public class ApplicationUsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityService _identityService;

    public ApplicationUsersController(IMediator mediator, UserManager<ApplicationUser> userManager, IdentityService identityService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _identityService = identityService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var result = await UserService.GetUserByIdAsync(_mediator, _userManager, id);
        if (result == null) return NotFound();

        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var result = await UserService.UpdateUserAsync(_mediator, _userManager, request);
        if (!result.Equals("Successfully updated")) return BadRequest(result);

        return Ok(result);
    }
    
    [HttpPost("getRoleFromToken")]
    public async Task<IActionResult> GetRole([FromBody] TokenRequest request)
    {
        var role = _identityService.GetRoleFromToken(request.Token);
        if (role == null || string.IsNullOrEmpty(role)) return BadRequest("Invalid token or role not found.");

        return Ok(role);
    }
}