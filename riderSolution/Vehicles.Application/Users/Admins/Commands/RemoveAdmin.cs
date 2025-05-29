using MediatR;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.Admins.Commands;

public record RemoveAdmin(int Id) : IRequest;

public class RemoveAdminHandler : IRequestHandler<RemoveAdmin>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAdminHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveAdmin request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var admin = await _unitOfWork.AdminRepository.GetByIdAsync(request.Id);
        if (admin != null)
        {
            await _unitOfWork.AdminRepository.RemoveAsync(request.Id);
            await _unitOfWork.SaveAsync();
        }
    }
}