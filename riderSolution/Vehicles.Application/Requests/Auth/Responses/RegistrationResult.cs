using System.Security.Claims;

namespace Vehicles.Application.Requests.Auth.Responses;

public class RegistrationResult
{
    public List<Claim> Claims { get; set; } = [];
    public string Role { get; set; } = string.Empty;
}
