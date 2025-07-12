namespace Vehicles.API.Contracts.DTOs.Users;

public class AuthResultDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}