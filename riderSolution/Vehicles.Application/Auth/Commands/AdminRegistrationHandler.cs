using System.Security.Claims;
using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Application.Users.Admins.Commands;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Auth.Commands;

public class AdminRegistrationHandler : IRegistrationHandler
{
    private readonly IMediator _mediator;

    public string Type => "admin";

    public AdminRegistrationHandler(IMediator mediator)
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

        await _mediator.Send(new CreateAdmin(identity.Id, request.FirstName!, request.LastName!));

        return new RegistrationResult
        {
            Claims = claims,
            Role = "Admin"
        };
    }
}
