namespace project1.Lesson_14_Assignment;

public class Lesson14Main
{
    private static CoffeeFactory _espressoFactory = new EspressoFactory();
    private static CoffeeFactory _cappuccinoFactory = new CappuccinoFactory();
    private static CoffeeFactory _flatWhiteFactory = new FlatWhiteFactory();

    static void AddSugar(Coffee coffee)
    {
        Console.WriteLine("How many sugar do you want to add?");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int amount))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        
        coffee.AddSugar(amount);
        Console.WriteLine("Sugar was added.\n");
    }
    static void AddMilk(Coffee coffee)
    {
        Console.WriteLine("How many milk do you want to add?");
        string input = Console.ReadLine();
        if (!int.TryParse(input, out int amount))
        {
            Console.WriteLine("Invalid input.");
            return;
        }
        
        Console.WriteLine("What type milk do you want to add?");
        input = Console.ReadLine();
        
        bool wasAdded = coffee.AddMilk(amount, input);
        if (wasAdded) Console.WriteLine("Milk was added.\n");
        else Console.WriteLine("We don't have this milk type.\n");
    }
    static Coffee OptionEspresso()
    {
        return _espressoFactory.CreateCoffee();
    }

    static Coffee OptionCappuccino()
    {
        return _cappuccinoFactory.CreateCoffee();
    }

    static Coffee OptionFlatWhite()
    {
        return _flatWhiteFactory.CreateCoffee();
    }
    
    static void PrintMenu()
    {
        Console.WriteLine("1. Create Espresso");
        Console.WriteLine("2. Create Cappuccino");
        Console.WriteLine("3. Create Flat White");
        Console.WriteLine("0. Exit\n");
    }
    
    internal static void MainFunction()
    {
        while (true)
        {
            PrintMenu();
            Console.Write("Enter choice: ");
            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }

            Coffee coffee;
            
            if (choice == 0) break;
            if (choice == 1) coffee = OptionEspresso();
            else if (choice == 2) coffee = OptionCappuccino();
            else if (choice == 3) coffee = OptionFlatWhite();
            else
            {
                Console.WriteLine("Unknown option. Try again.");
                continue;
            }
            
            Console.WriteLine("Do you want to add sugar? (Y/n)");
            input = Console.ReadLine().ToLower();
            if (input == "y") AddSugar(coffee);
            
            Console.WriteLine("Do you want to add more milk? (Y/n)");
            input = Console.ReadLine().ToLower();
            if (input == "y") AddMilk(coffee);
            
            Console.WriteLine($"Your coffee is made: {coffee}");
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}