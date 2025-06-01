using MediatR;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record RemoveUser(int Id) : IRequest;

public class RemoveUserHandler : IRequestHandler<RemoveUser>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveUser request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null) return;

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.RemoveAsync(request.Id);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}