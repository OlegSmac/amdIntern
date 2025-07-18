using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Requests.Users.Admins.Queries;

public record GetAdminById(string Id) : IRequest<Admin>;

public class GetAdminByIdHandler : IRequestHandler<GetAdminById, Admin>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAdminByIdHandler> _logger;

    public GetAdminByIdHandler(IUnitOfWork unitOfWork, ILogger<GetAdminByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Admin> Handle(GetAdminById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAdminById was called");
        ArgumentNullException.ThrowIfNull(request);

        Admin? admin = await _unitOfWork.AdminRepository.GetByIdAsync(request.Id);
        if (admin == null) throw new KeyNotFoundException($"Admin with id: {request.Id} not found");
        
        return admin;
    }
}