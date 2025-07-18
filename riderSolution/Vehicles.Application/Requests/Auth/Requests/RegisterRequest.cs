namespace Vehicles.Application.Requests.Auth.Requests;

public class RegisterRequest
{
    public string Type { get; set; } = null!; // "User", "Company", or "Admin"

    // Common fields
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Phone { get; set; } = null!;

    // User/Admin fields
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    // Company fields
    public string? Name { get; set; }
    public string? Description { get; set; }
}
