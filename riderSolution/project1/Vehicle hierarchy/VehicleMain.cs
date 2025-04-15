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
        // PrintAllCarInformation printCarInfo = delegate(Car car) //anonymous function 
        // {
        //     Console.WriteLine($"This is a car. Id: {car.GetId()}, Brand: {car.Info.Brand}, Model: {car.Info.Model}, Year: {car.Info.Year}, Passengers: {car.Passengers}, Max speed: {car.MaxSpeed}");
        // };
        //
        // var car3 = new Car(3, "Volkswagen", "Golf 3", 1994);
        // printCarInfo(car3);
        //
        // PrintAllCarInformation printCarInfo2 = (Car car) => Console.WriteLine($"This is a car. Id: {car.GetId()}, Brand: {car.Info.Brand}, Model: {car.Info.Model}, Year: {car.Info.Year}, Passengers: {car.Passengers}, Max speed: {car.MaxSpeed}");
        // printCarInfo2(car3);
        // Console.WriteLine($"Vehicle is{(car3.IsTurnedOffAndStopped() ? "" : "'n")} turned off and stopped.");
        // Console.WriteLine();
        //
        // var dealership3 = new VehicleDealership<Vehicle>();
        // dealership3.Add(car3);
        // dealership3.Add(new Car(4, "Mercedes", "C-Class", 2003));
        // dealership3.Add(new Car(5, "Mercedes", "S-Class", 2005));
        // dealership3.Add(new Truck(6, "Scania", "R344", 2014, 3000));
        //
        // var stoppedAndTurnedOffVehicles = dealership3.Find(delegate(Vehicle vehicle) { return vehicle.Speed == 0 && !vehicle.IsTurnedOn; });
        // foreach (var vehicle in stoppedAndTurnedOffVehicles) Console.WriteLine(vehicle.GetInfo());
        // Console.WriteLine();
        //
        // var merdeceses = dealership3.Find(v => v.Info.Brand == "Mercedes");
        // foreach (var merdeces in merdeceses) Console.WriteLine(merdeces.GetInfo());
        // Console.WriteLine();
        //
        // var brandSort = dealership3.GetSorted(v => v.Info.Brand);
        // foreach (var vehicle in brandSort) Console.WriteLine(vehicle.GetInfo());
        // Console.WriteLine();
        //
        // dealership3.PrintAllBrandsInDealership();
        // foreach (var vehicle in dealership3.FindAll().OnlyTurnedOffAndStopped()) Console.WriteLine(vehicle.GetInfo());
        // Console.WriteLine();
        
        
        //--------------------------------------------- 
        Customer oleg = new Customer { Name = "Oleg", FavoriteBrand = "Volkswagen" };
        Customer nikita = new Customer { Name = "Nikita", FavoriteBrand = "Mercedes" };
        Customer vova = new Customer { Name = "Vladimir", FavoriteBrand = "Mercedes" };

        List<Customer> customers = new List<Customer> { oleg, nikita, vova };

        VehicleDealership<Vehicle> dealership = new VehicleDealership<Vehicle>();
        var car4 = new Car(4, "Volkswagen", "Golf 3", 1994);
        var car5 = new Car(5, "Volkswagen", "Passat B5", 2001);
        var car6 = new Car(6, "Mercedes", "W203", 1998);
        var car7 = new Car(7, "Mercedes", "W201", 1996);
        var car8 = new Car(8, "Mercedes", "E500", 1985);
        dealership.Add(car4);
        dealership.Add(car5);
        dealership.Add(car6);
        dealership.Add(car7);
        dealership.Add(car8);

        var dealershipVehicles = dealership.FindAll();

        var customersAndFavoriteVehicles = customers.Join(dealershipVehicles,
            customer => customer.FavoriteBrand,
            vehicle => vehicle.Info.Brand,
            (customer, vehicle) => new
            {
                Name = customer.Name,
                Vehicle = vehicle.Info.GetInfo()
            });

        foreach (var item in customersAndFavoriteVehicles)
        {
            Console.WriteLine($"{item.Name} - {item.Vehicle}");
        }
        Console.WriteLine();

        var groupCustomersAndFavoriteVehicles = customers.GroupJoin(dealershipVehicles,
            customer => customer.FavoriteBrand,
            vehicle => vehicle.Info.Brand,
            (customer, vehicle) => new
            {
                Name = customer.Name,
                Vehicle = vehicle.Select(vehicle => vehicle.Info.GetInfo())
            });

        foreach (var item in groupCustomersAndFavoriteVehicles)
        {
            Console.WriteLine($"{item.Name}:");
            foreach (var vehicle in item.Vehicle)
            {
                Console.WriteLine($"- {vehicle}");
            }
        }
        Console.WriteLine();
        
        var groupVehiclesByBrand = dealershipVehicles.GroupBy(vehicle => vehicle.Info.Brand)
            .Select(vehicle => new
            {
                Brand = vehicle.Key,
                Count = vehicle.Count(),
                Vehicles = vehicle.Select(vehicle => vehicle.Info.GetInfo())
            }).ToList();

        foreach (var brand in groupVehiclesByBrand)
        {
            Console.WriteLine($"Brand: {brand.Brand}");
            Console.WriteLine($"Count: {brand.Count}");
            foreach (var vehicle in brand.Vehicles)
            {
                Console.WriteLine($"- {vehicle}");
            }
            Console.WriteLine();
        }
    }
}