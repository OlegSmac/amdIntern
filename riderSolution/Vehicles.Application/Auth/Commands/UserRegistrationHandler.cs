using System.Security.Claims;
using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Application.Users.Users.Commands;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Auth.Commands;

public class UserRegistrationHandler : IRegistrationHandler
{
    private readonly IMediator _mediator;

    public string Type => "user";

    public UserRegistrationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request, ApplicationUser identity)
    {
        var claims = new List<Claim>
        {
            new("FirstName", request.FirstName!),
            new("LastName", request.LastName!)
        };

        await _mediator.Send(new CreateUser(identity.Id, request.FirstName!, request.LastName!));

        return new RegistrationResult
        {
            Claims = claims,
            Role = "User"
        };
    }
}
