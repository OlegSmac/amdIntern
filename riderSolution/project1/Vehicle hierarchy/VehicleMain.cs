using System.Reflection.Emit;

namespace project1;

public class VehicleMain
{
    internal static void MainFunction()
    {
        var dealership1 = new VehicleDealership<Vehicle>();
        
        var car1 = new Car(1, "Volkswagen", "Golf 3", 1994);
        var car2 = new Car(2, "Volkswagen", "Passat B4", 1997);
        
        dealership1.Add(car1);
        dealership1.Add(car2);

        var truck1 = new Truck(3, "Scania", "R22", 2004, 1000);
        dealership1.Add(truck1);

        var car = dealership1.GetById(1);
        dealership1.ExcludeFromDealership(car);
        Console.WriteLine(car.GetInfo());
        
        var dealership2 = new VehicleDealership<Car>();
    }
}