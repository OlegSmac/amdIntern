using Vehicles.Domain.Users.Models;

namespace Vehicles.Application.Users.RegularUsers.Responses;

public class RegularUserDto
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public static RegularUserDto FromRegularUser(RegularUser regularUser)
    {
        return new RegularUserDto()
        {
            Id = regularUser.Id,
            Name = regularUser.Name,
            Email = regularUser.Email,
            Password = regularUser.Password
        };
    }
}