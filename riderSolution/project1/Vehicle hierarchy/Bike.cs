using project1.Exceptions;

namespace project1;

public class Bike : Vehicle
{
    public override int MaxSpeed { get; } = 180;
    private bool HasSidecar { get; set; }
    public Bike(int id, string brand, string model, int year, bool hasSidecar = false) : base(id, brand, model, year)
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
        var clonedBike = (Bike)base.Clone();
        
        return clonedBike;
    }

    public override string GetInfo()
    {
        return $"This is the bike {Info.Brand} {Info.Model} {Info.Year} and it has" + (HasSidecar ? "" : "'t") + " sidecar.";
    }
}