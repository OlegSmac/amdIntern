using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicles.Domain.Users.Models;

namespace Vehicles.Domain.Users.Relations;

public class Subscription
{
    [Required]
    [ForeignKey(nameof(User))]
    public string UserId { get; set; }
    public User User { get; set; }

    [Required]
    [ForeignKey(nameof(Company))]
    public string CompanyId { get; set; }
    public Company Company { get; set; }
}
