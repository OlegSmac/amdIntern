using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Admins.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Commands;

public record CreateAdmin(string Name, string Email, string Password) : IRequest<AdminDto>;

public class CreateAdminHandler : IRequestHandler<CreateAdmin, AdminDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAdminHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AdminDto> Handle(CreateAdmin request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var admin = new Admin()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        
        await _unitOfWork.AdminRepository.CreateAsync(admin);
        await _unitOfWork.SaveAsync();
        
        return AdminDto.FromAdmin(admin);
    }
}