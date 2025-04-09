namespace project1;

public class Car : Vehicle
{
    public override int MaxSpeed { get; } = 200;
    private int Passengers { get; set; } //driver is by default, this is extra passengers

    public Car(string brand, string model, int year, int passengers = 0) : base(brand, model, year)
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
        return $"This is the car {_info.Brand} {_info.Model} from {_info.Year} year with {Passengers} passengers.";
    }
}