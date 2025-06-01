using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.Admins.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Commands;

public record UpdateAdmin(int Id, string Name, string Email, string Password) : IRequest<AdminDto>;

public class UpdateAdminHandler : IRequestHandler<UpdateAdmin, AdminDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAdminHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task UpdateAdminAsync(Admin admin, UpdateAdmin request)
    {
        admin.Name = request.Name;
        admin.Email = request.Email;
        admin.Password = request.Password;
    }

    public async Task<AdminDto> Handle(UpdateAdmin request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var admin = await _unitOfWork.AdminRepository.GetByIdAsync(request.Id);
        if (admin is null) throw new KeyNotFoundException($"Admin with id: {request.Id} does not exist");
        
        await UpdateAdminAsync(admin, request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.AdminRepository.UpdateAsync(admin);
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