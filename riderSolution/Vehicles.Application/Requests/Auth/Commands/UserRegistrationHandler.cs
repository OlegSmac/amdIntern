using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Requests.Auth.Requests;
using Vehicles.Application.Requests.Auth.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Requests.Auth.Commands;

public class UserRegistrationHandler : IRegistrationHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRegistrationHandler> _logger;

    public string Type => "user";

    public UserRegistrationHandler(IUnitOfWork unitOfWork, ILogger<UserRegistrationHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    private async Task<User> CreateUserAsync(RegisterRequest request, ApplicationUser identity)
    {
        return new User()
        {
            Id = identity.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request, ApplicationUser identity)
    {
        _logger.LogInformation("CreateUser was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            User user = await CreateUserAsync(request, identity);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.CreateAsync(user);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }

        return new RegistrationResult
        {
            Claims = new List<Claim>
            {
                new("FirstName", request.FirstName!),
                new("LastName", request.LastName!)
            },
            Role = "User"
        };
    }
}
