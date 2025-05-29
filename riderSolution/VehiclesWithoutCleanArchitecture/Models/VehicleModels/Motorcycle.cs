using Vehicles.Exceptions;

namespace Vehicles.Models.VehicleModels;

public class Motorcycle : Vehicle
{
    public override int MaxSpeed { get; } = 180;
    public bool HasSidecar { get; set; }
    public Motorcycle(int id, string brand, string model, int year, bool hasSidecar = false) : base(id, brand, model, year)
    {
        HasSidecar = hasSidecar;
    }

    public void PutSidecar()
    {
        if (Speed > 0) throw new Exception("Bike should be stopped before putting sidecar.");
        if (HasSidecar) throw new Exception("Sidecar is already present.");
        
        HasSidecar = true;
    }

    public void RemoveSidecar()
    {
        if (Speed > 0) throw new Exception("Bike should be stopped before removing sidecar.");
        if (!HasSidecar) throw new Exception("Sidecar isn't present now.");
        
        HasSidecar = false;
    }

    public override object Clone() 
    {
        var clonedMotorcycle = (Motorcycle)base.Clone();
        
        return clonedMotorcycle;
    }

    public override string GetInfo()
    {
        return $"This is the motorcycle {Info.Brand} {Info.Model} {Info.Year} and it has" + (HasSidecar ? "" : "'t") + " sidecar.";
    }
}