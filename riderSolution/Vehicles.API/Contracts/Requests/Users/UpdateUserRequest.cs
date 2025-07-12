namespace Vehicles.API.Contracts.Requests.Users;

public class UpdateUserRequest
{
    public string Id { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? CompanyName { get; set; }
    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
}
