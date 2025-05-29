using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.RegularUsers.Responses;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record UpdateUser(int Id, string Name, string Email, string Password) : IRequest<RegularUserDto>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, RegularUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RegularUserDto> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user is null) throw new KeyNotFoundException($"User with id: {request.Id} does not exist.");
        
        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = request.Password;
        
        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveAsync();
        
        return RegularUserDto.FromRegularUser(user);
    }
}