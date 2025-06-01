using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.RegularUsers.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record UpdateUser(int Id, string Name, string Email, string Password) : IRequest<RegularUserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, RegularUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task UpdateUserAsync(RegularUser user, UpdateUser request)
    {
        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = request.Password;
    }

    public async Task<RegularUserDto> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user is null) throw new KeyNotFoundException($"User with id: {request.Id} does not exist.");
        
        await UpdateUserAsync(user, request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RegularUserDto.FromRegularUser(user);
    }
}