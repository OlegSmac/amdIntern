namespace project1;

public class Truck : Vehicle
{
    public override int MaxSpeed { get; } = 150;
    public double MaxLoadKg { get; set; }
    
    public Truck(string brand, string model, int year, double maxLoadKg) : base(brand, model, year)
    {
        MaxLoadKg = maxLoadKg;
    }

    public new object Clone()
    {
        var clonedTruck = (Truck)base.Clone();
        clonedTruck.MaxLoadKg = this.MaxLoadKg;
        
        return clonedTruck;
    }
    
    public override string GetInfo()
    {
        return $"This is the truck {Brand} {Model} from {Year} year with max load {MaxLoadKg} kg.";
    }
}