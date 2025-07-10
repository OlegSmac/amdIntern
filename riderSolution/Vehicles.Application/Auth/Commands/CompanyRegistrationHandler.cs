using System.Security.Claims;
using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Application.Users.Companies.Commands;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Auth.Commands;

public class CompanyRegistrationHandler : IRegistrationHandler
{
    private readonly IMediator _mediator;

    public string Type => "company";

    public CompanyRegistrationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<RegistrationResult> RegisterAsync(RegisterRequest request, ApplicationUser identity)
    {
        var claims = new List<Claim>
        {
            new("Name", request.Name!),
            new("Description", request.Description!)
        };

        await _mediator.Send(new CreateCompany(identity.Id, request.Name!, request.Description!));

        return new RegistrationResult
        {
            Claims = claims,
            Role = "Company"
        };
    }
}
