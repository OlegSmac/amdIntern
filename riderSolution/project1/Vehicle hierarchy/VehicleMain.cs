using System.Reflection.Emit;
using project1.Exceptions;
using project1.Extensions;

namespace project1;

public class VehicleMain
{

    public delegate void PrintAllCarInformation(Car car);
    internal static void MainFunction()
    {
//         var dealership1 = new VehicleDealership<Vehicle>();
//         
//         var car1 = new Car(1, "Volkswagen", "Golf 3", 1994);
//         var car2 = new Car(2, "Volkswagen", "Passat B4", 1997);
//
//         try
//         {
//             car1.TakePassengers(2);
//             dealership1.Add(car1);
//         }
//         catch (VehicleNotStoppedException e)
//         {
//             car1.Stop();
//             dealership1.Add(car1);
//         }
//         catch (VehicleTurnedOnException e)
//         {
//             car1.TurnOff();
//             dealership1.Add(car1);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine("There is another exception:\n" + e.Message);
//             //throw; //the exception can be rethrow to the next layer
//         }
//         finally
//         {
//             Vehicle vehicleInDealership = dealership1.GetById(car1.GetId());
// #if DEBUG
//             Console.WriteLine($"----The vehicle {car1.Info.Brand} {car1.Info.Model} was{(vehicleInDealership == null ? "'t" : "")} added to the dealership1.");
// #endif
//         }
//
//         var truck1 = new Truck(3, "Scania", "R22", 2004, 1000);
//         var car = dealership1.GetById(1);
//
//         try
//         {
//             dealership1.ExcludeFromDealership(car);
//         }
//         catch (ArgumentException e)
//         {
//             Console.WriteLine(e.Message);
//         }
//
//         var dealership2 = new VehicleDealership<Car>();
        
        
        //---------------------------------------------
        PrintAllCarInformation printCarInfo = delegate(Car car) //anonymous function 
        {
            Console.WriteLine($"This is a car. Id: {car.GetId()}, Brand: {car.Info.Brand}, Model: {car.Info.Model}, Year: {car.Info.Year}, Passengers: {car.Passengers}, Max speed: {car.MaxSpeed}");
        };
        
        var car3 = new Car(3, "Volkswagen", "Golf 3", 1994);
        printCarInfo(car3);
        
        PrintAllCarInformation printCarInfo2 = (Car car) => Console.WriteLine($"This is a car. Id: {car.GetId()}, Brand: {car.Info.Brand}, Model: {car.Info.Model}, Year: {car.Info.Year}, Passengers: {car.Passengers}, Max speed: {car.MaxSpeed}");
        printCarInfo2(car3);
        Console.WriteLine($"Vehicle is{(car3.IsTurnedOffAndStopped() ? "" : "'n")} turned off and stopped.");
        Console.WriteLine();

        var dealership3 = new VehicleDealership<Vehicle>();
        dealership3.Add(car3);
        dealership3.Add(new Car(4, "Mercedes", "C-Class", 2003));
        dealership3.Add(new Car(5, "Mercedes", "S-Class", 2005));
        dealership3.Add(new Truck(6, "Scania", "R344", 2014, 3000));
        
        var stoppedAndTurnedOffVehicles = dealership3.Find(delegate(Vehicle vehicle) { return vehicle.Speed == 0 && !vehicle.IsTurnedOn; });
        foreach (var vehicle in stoppedAndTurnedOffVehicles) Console.WriteLine(vehicle.GetInfo());
        Console.WriteLine();
        
        var merdeceses = dealership3.Find(v => v.Info.Brand == "Mercedes");
        foreach (var merdeces in merdeceses) Console.WriteLine(merdeces.GetInfo());
        Console.WriteLine();
        
        var brandSort = dealership3.GetSorted(v => v.Info.Brand);
        foreach (var vehicle in brandSort) Console.WriteLine(vehicle.GetInfo());
        Console.WriteLine();

        dealership3.PrintAllBrandsInDealership();
        foreach (var vehicle in dealership3.FindAll().OnlyTurnedOffAndStopped()) Console.WriteLine(vehicle.GetInfo());
        Console.WriteLine();
    }
}