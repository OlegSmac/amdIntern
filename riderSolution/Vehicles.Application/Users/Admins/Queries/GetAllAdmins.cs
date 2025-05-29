using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Admins.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Queries;

public record GetAllAdmins() : IRequest<List<AdminDto>>;

public class GetAllAdminsHandler : IRequestHandler<GetAllAdmins, List<AdminDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAdminsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AdminDto>> Handle(GetAllAdmins request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        List<Admin> admins = await _unitOfWork.AdminRepository.GetAllAsync();
        
        return admins.Select(AdminDto.FromAdmin).ToList();
    }
}