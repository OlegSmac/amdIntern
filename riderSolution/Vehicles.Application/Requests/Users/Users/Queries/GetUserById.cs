using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Requests.Users.Users.Queries;

public record GetUserById(string Id) : IRequest<User>;

public class GetUserByIdHandler : IRequestHandler<GetUserById, User>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetUserByIdHandler> _logger;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, ILogger<GetUserByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<User> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetUserById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null) throw new KeyNotFoundException($"User with id: {request.Id} not found");

        return user;
    }
}