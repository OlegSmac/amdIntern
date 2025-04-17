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

    public async Task Add(T vehicle)
    {
        try
        {
            if (GetById(vehicle.GetId()) != null)
                throw new VehicleAlreadyExistsException(
                    $"Vehicle with id = {vehicle.GetId()} already exists in dealership.");
            if (vehicle.Speed != 0) throw new Exception("Vehicle should be stopped before adding in dealership.");
            if (vehicle.IsTurnedOn) throw new Exception("Vehicle should be turned off before adding in dealership.");

            if (vehicle is Car car && car.Passengers > 0) throw new Exception("Car shouldn't have passengers.");
            if (vehicle is Truck truck && truck.HasCargo()) throw new Exception("Truck shouldn't have cargo.");

            _vehicles.Add(vehicle);
            
            await Logger.Log("Add", true);
        }
        catch (Exception e)
        {
            await Logger.Log("Add", false, e.Message);
            throw;
        }
    }

    public async Task ExcludeFromDealership(T vehicle)
    {
        try
        {
            if (vehicle == null) throw new ArgumentException($"Vehicle must not be null.");
            _vehicles.Remove(vehicle);
            
            await Logger.Log("ExcludeFromDealership", true);
        }
        catch (Exception e)
        {
            await Logger.Log("ExcludeFromDealership", false, e.Message);
            throw;
        }
    }

    public async Task Update(T vehicle)
    {
        try
        {
            if (vehicle == null) throw new ArgumentException($"Vehicle must not be null.");
            T vehicleInList = GetById(vehicle.GetId());

            if (vehicleInList == null)
                throw new VehicleNotFoundException(
                    $"Vehicle with id {vehicle.GetId()} must exist in dealership before updating.");
            if (vehicle.Speed != 0) throw new Exception("Vehicle should be stopped before adding in dealership.");
            if (vehicle.IsTurnedOn) throw new Exception("Vehicle should be turned off.");

            if (vehicle is Car car && car.Passengers > 0) throw new Exception("Car shouldn't has passengers.");
            if (vehicle is Truck truck && truck.HasCargo()) throw new Exception("Truck shouldn't have cargo.");

            if (vehicleInList.Speed != vehicle.Speed) vehicleInList.Speed = vehicle.Speed;
            if (vehicleInList.IsTurnedOn != vehicle.IsTurnedOn) vehicleInList.IsTurnedOn = vehicle.IsTurnedOn;
            vehicleInList.Info = vehicle.Info;
            
            await Logger.Log("Update", true);
        }
        catch (Exception e)
        {
            await Logger.Log("Update", false, e.Message);
            throw;
        }
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