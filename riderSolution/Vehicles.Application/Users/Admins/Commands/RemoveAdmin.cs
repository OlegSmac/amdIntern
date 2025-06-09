using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Users.Admins.Commands;

public record RemoveAdmin(int Id) : IRequest;

public class RemoveAdminHandler : IRequestHandler<RemoveAdmin>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RemoveAdminHandler> _logger;

    public RemoveAdminHandler(IUnitOfWork unitOfWork, ILogger<RemoveAdminHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(RemoveAdmin request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("RemoveAdmin was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            var admin = await _unitOfWork.AdminRepository.GetByIdAsync(request.Id);
            if (admin == null) return;
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.AdminRepository.RemoveAsync(request.Id);
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