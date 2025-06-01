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

    private async Task<RegularUser> CreateRegularUserAsync(CreateUser request)
    {
        return new RegularUser()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
    }

    public async Task<RegularUserDto> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RegularUser user = await CreateRegularUserAsync(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.CreateAsync(user);
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