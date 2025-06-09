using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Queries;

public record GetAllAdmins() : IRequest<List<Admin>>;

public class GetAllAdminsHandler : IRequestHandler<GetAllAdmins, List<Admin>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllAdminsHandler> _logger;

    public GetAllAdminsHandler(IUnitOfWork unitOfWork, ILogger<GetAllAdminsHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<Admin>> Handle(GetAllAdmins request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllAdmins was called");
        ArgumentNullException.ThrowIfNull(request);

        List<Admin> admins = await _unitOfWork.AdminRepository.GetAllAsync();

        return admins;
    }
}