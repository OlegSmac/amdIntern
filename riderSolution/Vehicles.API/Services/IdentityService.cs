using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vehicles.API.Options;

namespace Vehicles.API.Services;

public class IdentityService
{
    private readonly JwtSettings? _settings;
    private readonly byte[] _key;

    public IdentityService(IOptions<JwtSettings> jwtOptions)
    {
        _settings = jwtOptions.Value;
        ArgumentNullException.ThrowIfNull(_settings);
        ArgumentNullException.ThrowIfNull(_settings.SigningKey);
        ArgumentNullException.ThrowIfNull(_settings.Audiences);
        ArgumentNullException.ThrowIfNull(_settings.Audiences[0]);
        ArgumentNullException.ThrowIfNull(_settings.Issuer);
        _key = Encoding.ASCII.GetBytes(_settings?.SigningKey!);
    }

    private static JwtSecurityTokenHandler TokenHandler => new();

    public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
    {
        var tokenDescriptor = GetTokenDescriptor(identity);
        
        return TokenHandler.CreateToken(tokenDescriptor);
    }

    public string WriteToken(SecurityToken token)
    {
        return TokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
    {
        return new SecurityTokenDescriptor()
        {
            Subject = identity,
            Expires = DateTime.Now.AddHours(6),
            Audience = _settings!.Audiences?[0],
            Issuer = _settings.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
        };
    }
    
    public string? GetRoleFromToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = _settings!.Issuer,
            ValidAudiences = _settings.Audiences,
            IssuerSigningKey = new SymmetricSecurityKey(_key),
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = TokenHandler.ValidateToken(token, validationParameters, out _);
            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            return roleClaim?.Value;
        }
        catch
        {
            return null;
        }
    }

}