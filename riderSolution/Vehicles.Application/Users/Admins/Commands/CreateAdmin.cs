using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Commands;

public record CreateAdmin(string Id, string FirstName, string LastName) : IRequest<Admin>;

public class CreateAdminHandler : IRequestHandler<CreateAdmin, Admin>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateAdminHandler> _logger;

    public CreateAdminHandler(IUnitOfWork unitOfWork, ILogger<CreateAdminHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Admin> CreateAdminAsync(CreateAdmin request)
    {
        return new Admin()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
    }

    public async Task<Admin> Handle(CreateAdmin request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateAdmin was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Admin admin = await CreateAdminAsync(request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.AdminRepository.CreateAsync(admin);
                await _unitOfWork.SaveAsync();
            });
            
            return admin;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}