using Vehicles.Models.VehicleModels;
using Vehicles.Repositories;

namespace Vehicles.Services;

public class VehicleDealershipService<T> where T : Vehicle
{
    private readonly IVehicleRepository<T> _vehiclesRepository;

    public VehicleDealershipService(IVehicleRepository<T> vehiclesRepository)
    {
        _vehiclesRepository = vehiclesRepository;
    }

    public async Task SetVehiclesAsync(List<T> vehicles)
    {
        try
        {
            _vehiclesRepository.SetVehicles(vehicles);

            await Logger.Log("SetVehicles", true);
        }
        catch (Exception e)
        {
            await Logger.Log("SetVehicles", false, e.Message);
            throw;
        }
    }

    public T GetVehicleById(int id)
    {
        return _vehiclesRepository.FindById(id);
    }

    public IList<T> GetAllVehicles()
    {
        return _vehiclesRepository.FindAll();
    }

    public async Task AddVehicleAsync(T vehicle)
    {
        try
        {
            _vehiclesRepository.Add(vehicle);
                    
            await Logger.Log("AddVehicleAsync", true);
        }
        catch (Exception e)
        {
            await Logger.Log("AddVehicleAsync", false, e.Message);
            throw;
        }
    }

    public async Task ExcludeVehicleAsync(T vehicle)
    {
        try
        {
            _vehiclesRepository.ExcludeFromDealership(vehicle);
            
            await Logger.Log("ExcludeVehicleAsync", true);
        }
        catch (Exception e)
        {
            await Logger.Log("ExcludeVehicleAsync", false, e.Message);
            throw;
        }
    }

    public async Task UpdateVehicleAsync(T vehicle)
    {
        try
        {
            _vehiclesRepository.Update(vehicle);
            
            await Logger.Log("UpdateVehicleAsync", true);
        }
        catch (Exception e)
        {
            await Logger.Log("UpdateVehicleAsync", false, e.Message);
            throw;
        }
    }
}