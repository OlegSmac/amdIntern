using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Domain.Users.Relations;

public class Subscription
{
    [Required]
    public int UserId { get; set; }
    public User User { get; set; }

    [Required]
    public int CompanyId { get; set; }
    public Company Company { get; set; }
}
