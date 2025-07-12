using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Services;
using Vehicles.Domain.Users.Models;
using Vehicles.Application.Auth.Requests;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/google")]
public class GoogleAuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IdentityService _identityService;
    private readonly RegistrationService _registrationService;

    public GoogleAuthenticationController(UserManager<ApplicationUser> userManager, RegistrationService registrationService, IdentityService identityService)
    {
        _userManager = userManager;
        _identityService = identityService;
        _registrationService = registrationService;
    }
    
    [HttpGet("google-login")]
    public async Task GoogleLogin([FromQuery] string? prompt = null)
    {
        var redirectUri = Environment.GetEnvironmentVariable("REDIRECT_URI");
        var properties = new AuthenticationProperties
        {
            RedirectUri = "/api/accounts/google-response?returnUrl=" + Uri.EscapeDataString(redirectUri!)
        };

        if (!string.IsNullOrEmpty(prompt)) properties.Items["prompt"] = prompt; //new account registration
        
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, properties);
    }

    [HttpGet("google-response")]
    public async Task<IActionResult> GoogleResponse([FromQuery] string returnUrl)
    {
        var authenticateResult = await HttpContext.AuthenticateAsync("External");
        if (!authenticateResult.Succeeded) return BadRequest("Google authentication failed.");

        var claims = authenticateResult.Principal?.Claims;
        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(email)) return BadRequest("Email claim not found.");

        var user = await _registrationService.RegisterGoogleAsync(email, name);
        
        var roles = await _userManager.GetRolesAsync(user);
        var claimsIdentity = ClaimsService.CreateFromLogin(user, new List<Claim>(), roles);
        
        var token = _identityService.CreateSecurityToken(claimsIdentity);
        var jwtToken = _identityService.WriteToken(token);
        
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var finalRedirect = $"{returnUrl}?token={Uri.EscapeDataString(jwtToken)}" +
                            $"&id={Uri.EscapeDataString(user.Id)}" +
                            $"&name={Uri.EscapeDataString(user.UserName ?? user.Email)}" +
                            $"&email={Uri.EscapeDataString(email)}";

        return Redirect(finalRedirect);
    }
}