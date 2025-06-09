using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Users.Queries;

public record GetAllUsers() : IRequest<List<User>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers, List<User>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAllUsersHandler> _logger;

    public GetAllUsersHandler(IUnitOfWork unitOfWork, ILogger<GetAllUsersHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<List<User>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllUsers was called");
        ArgumentNullException.ThrowIfNull(request);
        
        List<User> users = await _unitOfWork.UserRepository.GetAllAsync();
        
        return users;
    }
}