using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Requests.Auth.Requests;

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
