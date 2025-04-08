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

        Vehicle truck = new Truck("Scania", "R580", 2020, 1000);
        Console.WriteLine("\n" + truck.GetInfo());
        truck.Drive("Spain");
        
        Console.WriteLine("\n\n");

        Vehicle anotherCar = (Car)car.Clone();
        anotherCar.Model = "Golf 2";
        car.Drive();
        anotherCar.Drive();
    }
}