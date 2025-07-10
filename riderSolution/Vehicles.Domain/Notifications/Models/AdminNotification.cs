using System.ComponentModel.DataAnnotations;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Domain.Notifications.Models;

public class AdminNotification : Notification
{
    [Required]
    public string AdminId { get; set; }
    public Admin Admin { get; set; }

    public bool IsResolved { get; set; } = false;
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public int Year { get; set; }
}