namespace project1;

public class Bike : Vehicle
{
    public override int MaxSpeed { get; } = 30;
    public bool HasLock { get; set; }
    public string FrameMaterial { get; set; }

    public Bike(string brand, string model, int year, bool hasLock, string frameMaterial) : base(brand, model, year)
    {
        HasLock = hasLock;
        FrameMaterial = frameMaterial;
    }

    public override object Clone() //new hides a method from the base class. We can't use override, because method isn't virtual 
    {
        var clonedBike = (Bike)base.Clone();
        clonedBike.HasLock = this.HasLock;
        clonedBike.FrameMaterial = this.FrameMaterial;
        
        return clonedBike;
    }

    public override string GetInfo()
    {
        return $"This is the bike {Brand} {Model} {Year} with {FrameMaterial} frame material";
    }
}