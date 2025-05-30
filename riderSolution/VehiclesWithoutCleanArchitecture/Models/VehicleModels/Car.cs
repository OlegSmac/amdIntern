using Vehicles.Exceptions;

namespace Vehicles.Models.VehicleModels;

public class Car : Vehicle
{
    public override int MaxSpeed { get; } = 200;
    public int Passengers { get; private set; } //driver is by default, this is extra passengers

    public Car(int id, string brand, string model, int year, int passengers = 0) : base(id, brand, model, year)
    {
        Passengers = passengers;
    }

    public void TakePassengers(int passengers)
    {
        if (Speed > 0) throw new Exception("Car should be stopped before taking any passengers.");
        if (Passengers + passengers > 4) throw new ArgumentException("Number of passengers can't be more than 4.");
        
        Passengers += passengers;
    }

    public void DisembarkPassengers(int passengers)
    {
        if (Speed > 0) throw new Exception("Car should be stopped before set down any passengers.");
        if (passengers > Passengers) throw new ArgumentException("Car doesn't have this number of passengers.");
        
        Passengers -= passengers;
    }

    public override object Clone()
    {
        var clonedCar = (Car)base.Clone();
        
        return clonedCar;
    }
    
    public override string GetInfo()
    {
        return $"This is the car {Info.Brand} {Info.Model} from {Info.Year} year with {Passengers} passengers.";
    }
}