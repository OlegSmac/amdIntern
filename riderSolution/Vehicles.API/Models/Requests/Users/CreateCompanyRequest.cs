namespace Vehicles.API.Models.Requests.Users;

public class CreateCompanyRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}