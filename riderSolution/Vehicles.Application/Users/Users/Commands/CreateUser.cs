using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Users.Commands;

public record CreateUser(string Name, string Email, string Password) : IRequest<User>;

public class CreateUserHandler : IRequestHandler<CreateUser, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(IUnitOfWork unitOfWork, ILogger<CreateUserHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<User> CreateRegularUserAsync(CreateUser request)
    {
        return new User()
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        };
    }

    public async Task<User> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateRegularUser was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            User user = await CreateRegularUserAsync(request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.CreateAsync(user);
                await _unitOfWork.SaveAsync();
            });
            
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}