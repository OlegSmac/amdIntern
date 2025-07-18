using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Vehicles.Application.Requests.Auth.Responses;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Services;

public class ClaimsService
{
    public static ClaimsIdentity CreateFromRegistration(ApplicationUser user, RegistrationResult result)
    {
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, result.Role)
        });

        claimsIdentity.AddClaims(result.Claims);
        return claimsIdentity;
    }

    public static ClaimsIdentity CreateFromLogin(ApplicationUser user, IList<Claim> claims, IList<string> roles)
    {
        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty)
        });

        claimsIdentity.AddClaims(claims);
        foreach (var role in roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return claimsIdentity;
    }
}