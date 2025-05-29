using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.RegularUsers.Responses;

namespace Vehicles.Application.Users.RegularUsers.Queries;

public record GetUserById(int Id) : IRequest<RegularUserDto>;

public class GetUserByIdHandler : IRequestHandler<GetUserById, RegularUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RegularUserDto> Handle(GetUserById request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null) throw new KeyNotFoundException($"User with id: {request.Id} not found");

        return RegularUserDto.FromRegularUser(user);
    }
}