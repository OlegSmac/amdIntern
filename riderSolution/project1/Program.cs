namespace project1;

public class Program
{
    static void Main()
    {
        Vehicle bike = new Bike("Aist", "Quest-D", 2018, true, "aluminium");
        Console.WriteLine(bike.GetInfo());
        bike.Drive(25);

        Vehicle car = new Car("Volkswagen", "Golf 3", 1994, "manual");
        Console.WriteLine("\n" + car.GetInfo());
        car.Drive();

        var truck = new Truck("Scania", "R580", 2020, 1000);
        Console.WriteLine("\n" + truck.GetInfo());
        truck.Drive("Spain");
        
        Console.WriteLine("\n\n");

        Vehicle anotherCar = (Car)car.Clone();
        anotherCar.Model = "Golf 2";
        car.Drive();
        anotherCar.Drive();

        if (truck is Truck)
        {
            truck.UploadTruck("sand", 22);
            Console.WriteLine($"There truck is uploaded with {truck.GetLoadKg()} kg.");
        }

        Vehicle vehicle = truck as Vehicle;
        vehicle.GetInfo();
        if (vehicle is not null) Console.WriteLine("Vehicle can be obtained from Truck.");

        vehicle.Model = "Another model";
        Console.WriteLine(truck.GetInfo());
        
        truck.UploadTruck(weight: 100, material: "stone");
        Console.WriteLine($"There truck is uploaded with {truck.GetLoadKg()} kg.");
    }
}