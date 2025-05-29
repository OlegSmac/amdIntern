using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Domain.Users.Models;

public class Admin
{
    public int Id { get; init; }
    
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}