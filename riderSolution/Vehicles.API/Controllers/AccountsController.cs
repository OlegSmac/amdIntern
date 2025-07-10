using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Models.DTOs.Users;
using Vehicles.API.Models.Requests.Users;
using Vehicles.API.Services;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.PaginationModels;
using Vehicles.Application.Users.Companies.Queries;
using Vehicles.Application.Users.Users.Commands;
using Vehicles.Application.Users.Users.Queries;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityService _identityService;
    private readonly RegistrationService _registrationService;

    public AccountsController(IMediator mediator,
        UserManager<ApplicationUser> userManager,
        IdentityService identityService,
        RegistrationService registrationService)
    {
        _mediator = mediator;
        _userManager = userManager;
        _identityService = identityService;
        _registrationService = registrationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _registrationService.RegisterAsync(request);
        if (result == null) return BadRequest("Registration failed");

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _registrationService.LoginAsync(loginRequest.Email, loginRequest.Password);
        if (result == null) return BadRequest("Invalid credentials");

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var result = await UserService.GetUserByIdAsync(_mediator, _userManager, id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost("getRoleFromToken")]
    public async Task<IActionResult> GetRole([FromBody] TokenRequest request)
    {
        var role = _identityService.GetRoleFromToken(request.Token);
        if (role == null || string.IsNullOrEmpty(role)) return BadRequest("Invalid token or role not found.");

        return Ok(role);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var result = await UserService.UpdateUserAsync(_mediator, _userManager, request);
        Console.WriteLine(result);
        if (!result.Equals("Successfully updated")) return BadRequest(result);

        return Ok(result);
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

    [HttpPost("getPagedCompanies")]
    public async Task<IActionResult> GetPagedCompanies([FromBody] PagedRequest request)
    {
        var command = new GetPagedCompanies(request.PageIndex, request.PageSize);
        var response = await _mediator.Send(command);

        var dtoItems = new List<CompanyDTO>();
        foreach (var company in response.Items)
        {
            var dto = await UserService.GetUserByIdAsync(_mediator, _userManager, company.Id);
            if (dto != null) dtoItems.Add((CompanyDTO)dto);
        }

        var result = new PaginatedResult<CompanyDTO>
        {
            Items = dtoItems,
            PageIndex = response.PageIndex,
            PageSize = response.PageSize,
            Total = response.Total
        };
        
        return Ok(result);
    }

    [HttpGet("getUserSubscriptions/{id}")]
    public async Task<IActionResult> GetUserSubscriptions([FromRoute] string id)
    {
        var command = new GetUserSubscriptions(id);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }

}
