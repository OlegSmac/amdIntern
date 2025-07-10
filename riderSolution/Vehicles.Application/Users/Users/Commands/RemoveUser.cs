using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.Users.Commands;

public record RemoveUser(string Id) : IRequest;

public class RemoveUserHandler : IRequestHandler<RemoveUser>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveUserHandler> _logger;

    public RemoveUserHandler(IUnitOfWork unitOfWork, ILogger<RemoveUserHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveUser request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemoveUser was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null) return;
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.RemoveAsync(request.Id);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}