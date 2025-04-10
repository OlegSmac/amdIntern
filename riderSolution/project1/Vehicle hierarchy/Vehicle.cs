using System;

namespace project1;

public abstract class Vehicle : ICloneable
{
    public struct VehicleInfo
    {
        public int Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public int Year { get; }

        public VehicleInfo(int id, string brand, string model, int year)
        {
            if (year > DateTime.Now.Year) throw new ArgumentException("Year cannot be in the future");

            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
        }
        
        public string GetInfo()
        {
            return $"This is {Brand} {Model} vehicle from {Year} year.";
        }
    }
    
    public VehicleInfo Info;
    public abstract int MaxSpeed { get; }
    public bool IsTurnedOn { get; set; }
    private int _speed = 0;

    public int Speed
    {
        get { return _speed; }
        set
        {
            if (!IsTurnedOn) throw new Exception("Vehicle is not turned on.");
            if (value > MaxSpeed) throw new ArgumentException($"Speed cannot be greater than {MaxSpeed}.");
            if (value < 0) throw new ArgumentException("Speed cannot be less than zero.");

            _speed = value;
        }
    }

    public int GetId()
    {
        return Info.Id;
    }

    public Vehicle(int id, string brand, string model, int year)
    {
        Info = new VehicleInfo(id, brand, model, year);
        IsTurnedOn = false;
    }
    
    public void TurnOn()
    {
        if (IsTurnedOn) throw new Exception("Vehicle is turned on.");
        
        IsTurnedOn = true;
    }

    public void TurnOff()
    {
        if (!IsTurnedOn) throw new Exception("Vehicle is not turned on.");
            
        IsTurnedOn = false;
    }

    public void ChangeSpeed(int speedDelta)
    {
        Speed += speedDelta; 
    }
    
    public void Drive() 
    {
        if (!IsTurnedOn) throw new Exception("Vehicle is not turned on.");
        
        if (Speed == 0) ChangeSpeed(1);
        Console.WriteLine($"The vehicle {Info.Brand} {Info.Model} is driving with speed {Speed} km/h.");        
    }

    public void Drive(int speed)
    {
        if (!IsTurnedOn) throw new Exception("Vehicle is not turned on.");
        
        Speed = speed;
        Console.WriteLine($"The vehicle {Info.Brand} {Info.Model} is driving with speed {Speed} km/h.");
    }

    public void Stop()
    {
        Speed = 0;
        Console.WriteLine($"The vehicle {Info.Brand} {Info.Model} is stopped.");
    }

    public virtual object Clone()
    {
        var clonedVehicle = (Vehicle)this.MemberwiseClone();
        clonedVehicle.Info = new VehicleInfo(Info.Id, Info.Brand, Info.Model, Info.Year);
    
        return clonedVehicle;
    }


    public virtual string GetInfo()
    {
        return Info.GetInfo();
    }
}