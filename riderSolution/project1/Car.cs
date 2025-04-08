namespace project1;

public class Car : Vehicle
{
    public override int MaxSpeed { get; } = 200;
    public string TransmissionType { get; set; }

    public Car(string brand, string model, int year, string transmissionType) : base(brand, model, year)
    {
        TransmissionType = transmissionType;
    }

    public override object Clone()
    {
        var clonedCar = (Car)base.Clone();
        clonedCar.TransmissionType = this.TransmissionType;
        
        return clonedCar;
    }
    
    public override string GetInfo()
    {
        return $"This is the car {Brand} {Model} from {Year} year with {TransmissionType} transmission type.";
    }
}