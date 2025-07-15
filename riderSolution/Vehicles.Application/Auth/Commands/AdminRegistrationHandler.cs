using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Application.Users.Admins.Commands;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Auth.Commands;

public class AdminRegistrationHandler : IRegistrationHandler
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdminRegistrationHandler> _logger;

    public string Type => "admin";

    public AdminRegistrationHandler(IUnitOfWork unitOfWork, ILogger<AdminRegistrationHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    private async Task<Admin> CreateAdminAsync(RegisterRequest request, ApplicationUser identity)
    {
        return new Admin()
        {
            Id = identity.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request, ApplicationUser identity)
    {
        _logger.LogInformation("CreateAdmin was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Admin admin = await CreateAdminAsync(request, identity);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.AdminRepository.CreateAsync(admin);
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
            Role = "Admin"
        };
    }
}
