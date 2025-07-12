using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.DTOs.Users;
using Vehicles.API.Contracts.Requests.Users;
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
    private readonly RegistrationService _registrationService;

    public AccountsController(RegistrationService registrationService)
    {
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

}
