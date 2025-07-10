using Microsoft.AspNetCore.Identity;
using Vehicles.API.DTOs.Responses;
using Vehicles.API.Services;
using Vehicles.Application.Auth.Commands;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Domain.Users.Models;

public class RegistrationService
{
    private readonly RegistrationHandlerFactory _factory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IdentityService _identityService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public RegistrationService(
        RegistrationHandlerFactory factory,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IdentityService identityService,
        SignInManager<ApplicationUser> signInManager)
    {
        _factory = factory;
        _userManager = userManager;
        _roleManager = roleManager;
        _identityService = identityService;
        _signInManager = signInManager;
    }

    public async Task<AuthResultDTO?> RegisterAsync(RegisterRequest request)
    {
        var user = CreateApplicationUser(request);

        var identityResult = await _userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded) return null;

        var result = await RegisterDomainUserAsync(request, user);
        await AssignRoleAndClaimsAsync(user, result);

        var claimsIdentity = ClaimsService.CreateFromRegistration(user, result);
        var token = _identityService.CreateSecurityToken(claimsIdentity);

        return new AuthResultDTO
        {
            Id = user.Id,
            Name = user.UserName ?? user.Email,
            Role = result.Role,
            Token = _identityService.WriteToken(token)
        };
    }

    public async Task<AuthResultDTO?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        if (!await IsPasswordValidAsync(user, password)) return null;

        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var claimsIdentity = ClaimsService.CreateFromLogin(user, claims, roles);
        var token = _identityService.CreateSecurityToken(claimsIdentity);

        return new AuthResultDTO
        {
            Id = user.Id,
            Name = user.UserName ?? user.Email,
            Role = roles.Contains("Admin") ? "Admin" : roles[0],
            Token = _identityService.WriteToken(token)
        };
    }

    private ApplicationUser CreateApplicationUser(RegisterRequest request)
    {
        return new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Username,
            PhoneNumber = request.Phone
        };
    }

    private async Task<RegistrationResult> RegisterDomainUserAsync(RegisterRequest request, ApplicationUser user)
    {
        var handler = _factory.GetHandler(request.Type);
        if (handler == null) throw new InvalidOperationException("Invalid registration type");
        
        return await handler.RegisterAsync(request, user);
    }

    private async Task AssignRoleAndClaimsAsync(ApplicationUser user, RegistrationResult result)
    {
        if (!await _roleManager.RoleExistsAsync(result.Role)) await _roleManager.CreateAsync(new IdentityRole(result.Role));

        await _userManager.AddClaimsAsync(user, result.Claims);
        await _userManager.AddToRoleAsync(user, result.Role);
    }

    private async Task<bool> IsPasswordValidAsync(ApplicationUser user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        return result.Succeeded;
    }
}
