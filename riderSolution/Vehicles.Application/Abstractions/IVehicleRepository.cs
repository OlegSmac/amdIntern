using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Abstractions;

public interface IVehicleRepository
{
    Task<Vehicle> CreateAsync(Vehicle vehicle);
    
    Task<Vehicle?> GetByIdAsync(int id);
    
    Task<List<Vehicle>> GetAllAsync();
    
    Task<List<Car>> GetAllCarsAsync();
    
    Task<List<Motorcycle>> GetAllMotorcyclesAsync();
    
    Task<List<Truck>> GetAllTrucksAsync();
    
    Task RemoveAsync(Vehicle vehicle);
    
    Task<Vehicle> UpdateAsync(Vehicle vehicle);
}