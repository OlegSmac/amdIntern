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

    private async Task<Admin> CreateAdminAsync(CreateAdmin request)
    {
        return new Admin()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
    }

    public async Task<AdminDto> Handle(CreateAdmin request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Admin admin = await CreateAdminAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.AdminRepository.CreateAsync(admin);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return AdminDto.FromAdmin(admin);
    }
}