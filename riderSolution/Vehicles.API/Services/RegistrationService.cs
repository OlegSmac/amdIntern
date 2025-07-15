using Microsoft.AspNetCore.Identity;
using Vehicles.API.Contracts.DTOs.Users;
using Vehicles.API.Services;
using Vehicles.Application.Auth.Commands;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Domain.Users.Models;

public class RegistrationService
{
    private readonly ILogger<RegistrationService> _logger;
    private readonly RegistrationHandlerFactory _factory;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IdentityService _identityService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public RegistrationService(
        ILogger<RegistrationService> logger,
        RegistrationHandlerFactory factory,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IdentityService identityService,
        SignInManager<ApplicationUser> signInManager)
    {
        _logger = logger;
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
        if (!identityResult.Succeeded)
        {
            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            _logger.LogError($"Failed to create user. Errors: {errors}");
            return null;
        }

        var result = await RegisterDomainUserAsync(request, user);
        if (result == null) return null;

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

    public async Task<ApplicationUser?> RegisterOrLoginGoogleAsync(string email, string? name)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null) return user;

        user = CreateGoogleApplicationUser(email, name);

        var identityResult = await _userManager.CreateAsync(user);
        if (!identityResult.Succeeded)
        {
            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            _logger.LogError($"Failed to create user. Errors: {errors}");
            return null;
        }

        var domainRequest = CreateGoogleRegisterRequest(email, name);
        var domainResult = await RegisterDomainUserAsync(domainRequest, user);
        if (domainResult == null)
        {
            _logger.LogError("Failed to create domain user.");
            return null;
        }

        return user;
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

    private ApplicationUser CreateGoogleApplicationUser(string email, string? name)
    {
        return new ApplicationUser
        {
            Email = email,
            UserName = (name ?? email).Replace(" ", "_"),
            EmailConfirmed = true
        };
    }

    private RegisterRequest CreateGoogleRegisterRequest(string email, string? name)
    {
        return new RegisterRequest
        {
            Type = "user",
            Email = email,
            Username = (name ?? email).Replace(" ", "_"),
            Password = Guid.NewGuid().ToString("N") + "!A", // Dummy strong password
            Phone = "",
            FirstName = name?.Split(' ').FirstOrDefault() ?? "Google",
            LastName = name?.Split(' ').Skip(1).FirstOrDefault() ?? "User"
        };
    }

    private async Task<RegistrationResult> RegisterDomainUserAsync(RegisterRequest request, ApplicationUser user)
    {
        var handler = _factory.GetHandler(request.Type);
        if (handler == null) throw new InvalidOperationException("Invalid registration type");
        
        var registration = await handler.RegisterAsync(request, user);
        await AssignRoleAndClaimsAsync(user, registration);

        return registration;
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
