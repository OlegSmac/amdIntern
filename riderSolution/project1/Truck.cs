namespace project1;

public class Truck : Vehicle
{
    public override int MaxSpeed { get; } = 150;
    public double MaxLoadKg { get; set; }

    private double _loadKg;
    
    private double LoadKg
    {
        set
        {
            if (value <= MaxLoadKg) _loadKg = value; 
        }
    }
    
    public Truck(string brand, string model, int year, double maxLoadKg) : base(brand, model, year)
    {
        MaxLoadKg = maxLoadKg;
        _loadKg = 0;
    }

    public double GetLoadKg()
    {
        return _loadKg;
    }

    public void UploadTruck(string material, double weight = 0)
    {
        _loadKg = Math.Min(this.MaxLoadKg, weight);
        Console.WriteLine($"The {weight} of {material} was uploaded.");
    }

    public override object Clone()
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