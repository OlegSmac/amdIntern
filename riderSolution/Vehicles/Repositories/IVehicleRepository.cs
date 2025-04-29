using Vehicles.Models.VehicleModels;

namespace Vehicles.Repositories;

public interface IVehicleRepository<T> where T : Vehicle
{
    IList<T> FindAll();
    T FindById(int id);
    void Add(T vehicle);
    void SetVehicles(List<T> vehicles);
    void ExcludeFromDealership(T vehicle);
    void Update(T vehicle);
}