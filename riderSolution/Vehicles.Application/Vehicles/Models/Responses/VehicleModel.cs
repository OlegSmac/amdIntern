using Vehicles.Domain.VehicleTypes.Models.VehicleModels;

namespace Vehicles.Application.Vehicles.Models.Responses;

public class VehicleModel
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}