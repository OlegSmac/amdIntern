using Vehicles.Models.VehicleModels;

namespace Vehicles.Extensions;

public static class VehicleExtensions
{
    public static bool IsTurnedOffAndStopped(this Vehicle vehicle)
    {
        return !vehicle.IsTurnedOn && vehicle.Speed == 0;
    }
    
    public static IEnumerable<Vehicle> OnlyTurnedOffAndStopped(this IEnumerable<Vehicle> vehicles)
    {
        return vehicles.Where(v => v.IsTurnedOffAndStopped());
    }
}