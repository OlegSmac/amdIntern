using project1.Exceptions;

namespace project1;

public class VehicleDealership<T> : IVehicleRepository<T> where T : Vehicle
{
    private readonly List<T> _vehicles;

    public VehicleDealership()
    {
        _vehicles = new List<T>();
    }

    public T GetById(int id)
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
        if (GetById(vehicle.GetId()) != null) throw new Exception($"Vehicle with id = {vehicle.GetId()} already exists in dealership.");
        if (vehicle.Speed != 0) throw new VehicleNotStoppedException("Vehicle should be stopped before adding in dealership.");
        if (vehicle.IsTurnedOn) throw new VehicleTurnedOnException("Vehicle should be turned off before adding in dealership.");
        
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
        T vehicleInList = GetById(vehicle.GetId()); 
        
        if (vehicleInList == null) throw new Exception($"Vehicle with {vehicle.GetId()} must exists before updating.");
        if (vehicle.Speed != 0) throw new VehicleNotStoppedException("Vehicle should be stopped before adding in dealership.");
        if (vehicle.IsTurnedOn) throw new VehicleTurnedOnException("Vehicle should be turned off.");
        
        if (vehicle is Car car && car.Passengers > 0) throw new Exception("Car shouldn't has passengers.");
        if (vehicle is Truck truck && truck.HasCargo()) throw new Exception("Truck shouldn't have cargo.");
        
        if (vehicleInList.Speed != vehicle.Speed) vehicleInList.Speed = vehicle.Speed;
        if (vehicleInList.IsTurnedOn != vehicle.IsTurnedOn) vehicleInList.IsTurnedOn = vehicle.IsTurnedOn;
        vehicleInList.Info = vehicle.Info;
    }

    // public List<T> Find(Func<T, bool> compare) //using Func delegate
    // {
    //     var vehicles = new List<T>();
    //     foreach (T vehicle in _vehicles)
    //     {
    //         if (compare(vehicle)) vehicles.Add(vehicle);
    //     }
    //     
    //     return vehicles;
    // }
    
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