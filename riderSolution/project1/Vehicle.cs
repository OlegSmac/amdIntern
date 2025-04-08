using System;

namespace project1;

public abstract class Vehicle : ICloneable
{
    public abstract int MaxSpeed { get; }
    public string Brand { get; set; }
    public string Model { get; set; }

    private int _year;

    public int Year
    {
        get { return _year; }
        set
        {
            if (value <= DateTime.Now.Year) _year = value;
        }
    }

    protected Vehicle(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }
    
    public void Drive()
    {
        Console.WriteLine($"The vehicle {Brand} {Model} is driving.");        
    }

    public void Drive(double distance)
    {
        Console.WriteLine($"The vehicle {Brand} {Model} is driving for {distance} km.");
    }

    public void Drive(string destination)
    {
        Console.WriteLine($"The vehicle {Brand} {Model} is driving to {destination}.");
    }

    public virtual object Clone()
    {
        return this.MemberwiseClone();
    }
    
    public virtual string GetInfo()
    {
        return $"This is {Brand} {Model} vehicle from {Year} year.";
    }
}