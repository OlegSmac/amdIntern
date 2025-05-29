using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.Admins.Responses;

public class AdminDto
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static AdminDto FromAdmin(Admin admin)
    {
        return new AdminDto()
        {
            Id = admin.Id,
            Name = admin.Name,
            Email = admin.Email,
            Password = admin.Password
        };
    }
}