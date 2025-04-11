using System.Reflection.Emit;
using project1.Exceptions;

namespace project1;

public class VehicleMain
{
    internal static void MainFunction()
    {
        var dealership1 = new VehicleDealership<Vehicle>();
        
        var car1 = new Car(1, "Volkswagen", "Golf 3", 1994);
        var car2 = new Car(2, "Volkswagen", "Passat B4", 1997);

        try
        {
            car1.TakePassengers(2);
            dealership1.Add(car1);
        }
        catch (VehicleNotStoppedException e)
        {
            car1.Stop();
            dealership1.Add(car1);
        }
        catch (VehicleTurnedOnException e)
        {
            car1.TurnOff();
            dealership1.Add(car1);
        }
        catch (Exception e)
        {
            Console.WriteLine("There is another exception:\n" + e.Message);
            //throw; //the exception can be rethrow to the next layer
        }
        finally
        {
            Vehicle vehicleInDealership = dealership1.GetById(car1.GetId());
#if DEBUG
            Console.WriteLine($"----The vehicle {car1.Info.Brand} {car1.Info.Model} was{(vehicleInDealership == null ? "'t" : "")} added to the dealership1.");
#endif
        }

        var truck1 = new Truck(3, "Scania", "R22", 2004, 1000);
        var car = dealership1.GetById(1);

        try
        {
            dealership1.ExcludeFromDealership(car);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

        var dealership2 = new VehicleDealership<Car>();
    }
}