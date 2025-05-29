using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.RegularUsers.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record CreateUser(string Name, string Email, string Password) : IRequest<RegularUserDto>;

public class CreateUserHandler : IRequestHandler<CreateUser, RegularUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RegularUserDto> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = new RegularUser()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
        
        await _unitOfWork.UserRepository.CreateAsync(user);
        await _unitOfWork.SaveAsync();
        
        return RegularUserDto.FromRegularUser(user);
    }
}