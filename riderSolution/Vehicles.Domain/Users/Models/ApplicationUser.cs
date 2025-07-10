using Microsoft.AspNetCore.Identity;

namespace Vehicles.Domain.Users.Models;

public class ApplicationUser : IdentityUser
{
    public virtual User? User { get; set; }
    public virtual Admin? Admin { get; set; }
    public virtual Company? Company { get; set; }
}