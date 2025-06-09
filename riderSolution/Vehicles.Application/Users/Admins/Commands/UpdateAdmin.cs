using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Commands;

public record UpdateAdmin(int Id, string Name, string Email, string Password) : IRequest<Admin>;

public class UpdateAdminHandler : IRequestHandler<UpdateAdmin, Admin>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateAdminHandler> _logger;

    public UpdateAdminHandler(IUnitOfWork unitOfWork, ILogger<UpdateAdminHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task UpdateAdminAsync(Admin admin, UpdateAdmin request)
    {
        admin.Name = request.Name;
        admin.Email = request.Email;
        admin.Password = request.Password;
    }

    public async Task<Admin> Handle(UpdateAdmin request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateAdmin was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdAsync(request.Id);
            if (admin is null) throw new KeyNotFoundException($"Admin with id: {request.Id} does not exist");
        
            await UpdateAdminAsync(admin, request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.AdminRepository.UpdateAsync(admin);
                await _unitOfWork.SaveAsync();
            });
            
            return admin;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}