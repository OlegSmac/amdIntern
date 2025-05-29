using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Users.RegularUsers.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.RegularUsers.Queries;

public record GetAllUsers() : IRequest<List<RegularUserDto>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers, List<RegularUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<RegularUserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        List<RegularUser> users = await _unitOfWork.UserRepository.GetAllAsync();
        
        return users.Select(RegularUserDto.FromRegularUser).ToList();
    }
}