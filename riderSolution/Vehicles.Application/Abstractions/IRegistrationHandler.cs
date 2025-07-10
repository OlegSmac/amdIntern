using Vehicles.Application.Auth.Requests;
using Vehicles.Application.Auth.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Abstractions;

public interface IRegistrationHandler
{
    string Type { get; }
    Task<RegistrationResult> RegisterAsync(RegisterRequest request, ApplicationUser identity);
}
