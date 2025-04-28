using project1.Lesson_14_Assignment.Enums;

namespace project1.Lesson_14_Assignment;

public class Lesson14Main
{
    private static CoffeeFactory _coffeeFactory = new();
    
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
        return _coffeeFactory.CreateCoffee(CoffeeType.Espresso);
    }

    static Coffee OptionCappuccino()
    {
        return _coffeeFactory.CreateCoffee(CoffeeType.Cappuccino);
    }

    static Coffee OptionFlatWhite()
    {
        return _coffeeFactory.CreateCoffee(CoffeeType.FlatWhite);
    }

    static void AdditionalOptionAddSugar(Coffee coffee)
    {
        Console.WriteLine("Do you want to add sugar? (Y/n)");
        string input = Console.ReadLine().ToLower();
        if (input == "y") AddSugar(coffee);
    }

    static void AdditionalOptionAddMilk(Coffee coffee)
    {
        Console.WriteLine("Do you want to add more milk? (Y/n)");
        string input = Console.ReadLine().ToLower();
        if (input == "y") AddMilk(coffee);
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
            
            AdditionalOptionAddSugar(coffee);
            AdditionalOptionAddMilk(coffee);
            
            Console.WriteLine($"Your coffee was made: {coffee}");
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}