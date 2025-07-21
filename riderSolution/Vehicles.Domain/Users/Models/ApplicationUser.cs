using Microsoft.AspNetCore.Identity;

namespace Vehicles.Domain.Users.Models;

public class ApplicationUser : IdentityUser
{
    public User? User { get; set; }
    public Admin? Admin { get; set; }
    public Company? Company { get; set; }
}