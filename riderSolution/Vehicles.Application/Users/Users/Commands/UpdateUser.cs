using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Users.Commands;

public record UpdateUser(int Id, string Name, string Email, string Password) : IRequest<User>;

public class UpdateUserHandler : IRequestHandler<UpdateUser, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateUserHandler> _logger;

    public UpdateUserHandler(IUnitOfWork unitOfWork, ILogger<UpdateUserHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task UpdateUserAsync(User user, UpdateUser request)
    {
        user.Name = request.Name;
        user.Email = request.Email;
        user.Password = request.Password;
    }

    public async Task<User> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateUser was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user is null) throw new KeyNotFoundException($"User with id: {request.Id} does not exist.");
        
            await UpdateUserAsync(user, request);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.UpdateAsync(user);
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