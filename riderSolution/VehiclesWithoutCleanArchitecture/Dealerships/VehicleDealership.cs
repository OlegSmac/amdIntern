using Vehicles.Exceptions;
using Vehicles.Models.VehicleModels;
using Vehicles.Models.UserModels;
using Vehicles.Repositories;

namespace Vehicles;

public class VehicleDealership<T> : IVehicleRepository<T> where T : Vehicle
{
    private List<T> _vehicles = new();

    public VehicleDealership()
    { }
    
    public VehicleDealership(List<T> vehicles)
    {
        _vehicles = vehicles;
    }
    
    public void SetVehicles(List<T> vehicles)
    {
        _vehicles = vehicles;
    }
    
    public T FindById(int id)
    {
        foreach (T vehicle in _vehicles)
        {
            if (vehicle.GetId() == id) return vehicle;
        }

        return null;
    }

    public IList<T> FindAll()
    {
        return _vehicles;
    }

    public void Add(T vehicle)
    {
        if (FindById(vehicle.GetId()) != null) throw new VehicleAlreadyExistsException($"Vehicle with id = {vehicle.GetId()} already exists in dealership.");
        if (vehicle.Speed != 0) throw new Exception("Vehicle should be stopped before adding in dealership.");
        if (vehicle.IsTurnedOn) throw new Exception("Vehicle should be turned off before adding in dealership.");

        if (vehicle is Car car && car.Passengers > 0) throw new Exception("Car shouldn't have passengers.");
        if (vehicle is Truck truck && truck.HasCargo()) throw new Exception("Truck shouldn't have cargo.");

        _vehicles.Add(vehicle);
    }

    public void ExcludeFromDealership(T vehicle)
    {
        if (vehicle == null) throw new ArgumentException($"Vehicle must not be null.");
        _vehicles.Remove(vehicle);
    }

    public void Update(T vehicle)
    {
        if (vehicle == null) throw new ArgumentException($"Vehicle must not be null.");
        T vehicleInList = FindById(vehicle.GetId());

        if (vehicleInList == null) throw new VehicleNotFoundException($"Vehicle with id {vehicle.GetId()} must exist in dealership before updating.");
        if (vehicle.Speed != 0) throw new Exception("Vehicle should be stopped before adding in dealership.");
        if (vehicle.IsTurnedOn) throw new Exception("Vehicle should be turned off.");

        if (vehicle is Car car && car.Passengers > 0) throw new Exception("Car shouldn't has passengers.");
        if (vehicle is Truck truck && truck.HasCargo()) throw new Exception("Truck shouldn't have cargo.");

        if (vehicleInList.Speed != vehicle.Speed) vehicleInList.Speed = vehicle.Speed;
        if (vehicleInList.IsTurnedOn != vehicle.IsTurnedOn) vehicleInList.IsTurnedOn = vehicle.IsTurnedOn;
        vehicleInList.Info = vehicle.Info;
    }
    
    public List<T> Find(Func<T, bool> compare)
    {
        return _vehicles.Where(compare).ToList();
    }
    
    public List<T> GetSorted(Func<T, object> selector)
    {
        return _vehicles.OrderBy(selector).ToList();
    }

    public void PrintAllBrandsInDealership()
    {
        var brands = _vehicles.Select(v => v.Info.Brand).Distinct();
     
        Console.WriteLine("Brands in the dealership:");
        foreach (var brand in brands) Console.Write($"{brand} ");
        Console.WriteLine();
    }

    public double VehiclesAverageYear()
    {
        return _vehicles.Average(vehicle => vehicle.Info.Year);
    }

    public IList<T> GetIntersectVehicles(VehicleDealership<T> vehicleDealership)
    {
        return _vehicles.Intersect(vehicleDealership.FindAll()).ToList();
    }

    public bool AreAllVehiclesFromBrand(string brand)
    {
        return _vehicles.Any(vehicle => vehicle.Info.Brand == brand);
    }

    public T GetFirstVehicleFromBrand(string brand)
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.Info.Brand == brand);
    }
}