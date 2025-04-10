namespace project1;

public record Cargo
{
    public string Material { get; init; }

    private int _weight;
    public int Weight
    {
        get { return _weight; }
        init
        {
            if (value <= 0) throw new Exception("Weight should be positive number.");
            
            _weight = value;
        }
    }

    public Cargo(string material, int weight)
    {
        Material = material;
        Weight = weight;
    }
}

public class Truck : Vehicle
{
    public override int MaxSpeed { get; } = 150;
    public int MaxLoadKg { get; }

    private Cargo _cargo;
    
    public Truck(int id, string brand, string model, int year, int maxLoadKg) : base(id, brand, model, year)
    {
        MaxLoadKg = maxLoadKg;
    }

    public string GetMaterial()
    {
        return _cargo == null ? "Empty" : _cargo.Material;
    } 
    
    public int GetLoadKg()
    {
        return _cargo == null ? 0 : _cargo.Weight; 
    }

    public bool HasCargo()
    {
        return _cargo != null;
    }

    public void UploadTruck(Cargo cargo)
    {
        if (Speed > 0) throw new Exception("Truck should be stopped before uploading.");
        if (_cargo != null) throw new Exception("Truck is already uploaded.");
        if (cargo.Weight > MaxLoadKg) throw new Exception("Weight cannot be greater than max load kg.");
        
        _cargo = cargo;
        Console.WriteLine($"The {_cargo.Weight} of {_cargo.Material} was uploaded.");
    }

    public void UnloadTruck()
    {
        if (Speed > 0) throw new Exception("Truck should be stopped before unloading.");
        if (_cargo == null) throw new Exception("Truck is already unloaded.");
        
        string material = _cargo.Material;
        int weight = _cargo.Weight;
        _cargo = null;
        
        Console.WriteLine($"The {weight} of {material} was unloaded.");
    }

    public override object Clone()
    {
        var clonedTruck = (Truck)base.Clone();
        clonedTruck._cargo = _cargo == null ? null : new Cargo(_cargo.Material, _cargo.Weight);
        
        return clonedTruck;
    }
    
    public override string GetInfo()
    {
        return $"This is the truck {Info.Brand} {Info.Model} from {Info.Year} year with max load {MaxLoadKg} kg.";
    }
}
