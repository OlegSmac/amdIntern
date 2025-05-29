using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Admins.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Queries;

public record GetAdminById(int Id) : IRequest<AdminDto>;

public class GetAdminByIdHandler : IRequestHandler<GetAdminById, AdminDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAdminByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AdminDto> Handle(GetAdminById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Admin? admin = await _unitOfWork.AdminRepository.GetByIdAsync(request.Id);
        if (admin == null) throw new KeyNotFoundException($"Admin with id: {request.Id} not found");
        
        return AdminDto.FromAdmin(admin);
    }
}