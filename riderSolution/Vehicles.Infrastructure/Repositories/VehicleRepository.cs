using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly VehiclesDbContext _context;

    public VehicleRepository(VehiclesDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> CreateAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
        return vehicle;
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles.ToListAsync();
    }

    public async Task<List<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }
    
    public async Task<List<Motorcycle>> GetAllMotorcyclesAsync()
    {
        return await _context.Motorcycles.ToListAsync();
    }
    
    public async Task<List<Truck>> GetAllTrucksAsync()
    {
        return await _context.Trucks.ToListAsync();
    }

    public async Task RemoveAsync(Vehicle vehicle)
    {
        var toDelete = await _context.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicle.Id);
        if (toDelete != null)
        {
            _context.Vehicles.Remove(toDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }
}