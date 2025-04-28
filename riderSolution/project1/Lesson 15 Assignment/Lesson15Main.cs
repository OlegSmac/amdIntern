using System.Reflection.Metadata;
using project1.Lesson_12_Assignment;
using project1.Lesson_15_Assignment.Orders;
using project1.Lesson_15_Assignment.Services;
using project1.Lesson_15_Assignment.Shops;
using project1.Lesson_15_Assignment.Users;
using UserService = project1.Lesson_15_Assignment.Services.UserService;

namespace project1.Lesson_15_Assignment;

public class Lesson15Main
{
    static int ParseInputNum()
    {
        string input = Console.ReadLine();
        int num;
    
        while (!int.TryParse(input, out num))
        {
            Console.WriteLine("Input must be a number. Try again.");
            input = Console.ReadLine();
        }

        return num;
    }

    static void OptionSubscribeCustomer()
    {
        Console.WriteLine("Write customer id:");
        int id = ParseInputNum();
        
        var customer = UserService.GetCustomer(id);
        if (customer != null)
        {
            BookService.Subscribe(customer, true);
            Console.WriteLine("Customer is subscribed.\n");
        }
        else Console.WriteLine("Customer is not subscribed. There isn't customer with this id.\n");
    }
    
    static void OptionSubscribeStaff()
    {
        Console.WriteLine("Write staff id:");
        int id = ParseInputNum();

        var staff = UserService.GetStaff(id);
        if (staff != null)
        {
            BookService.Subscribe(staff, false);
            Console.WriteLine("Staff is subscribed.\n");
        }
        else Console.WriteLine("Staff is not subscribed. There isn't staff member with this id.\n");
    }
    
    static void OptionUnsubscribeCustomer()
    {
        Console.WriteLine("Write customer id:");
        int id = ParseInputNum();

        var customer = UserService.GetCustomer(id);
        if (customer != null)
        {
            BookService.Unsubscribe(customer, true);
            Console.WriteLine("Customer is unsubscribed.\n");
        }
        else Console.WriteLine("Customer is not unsubscribed. There isn't customer with this id.\n");
    }

    static void OptionUnsubscribeStaff()
    {
        Console.WriteLine("Write staff id:");
        int id = ParseInputNum();

        var staff = UserService.GetStaff(id);
        if (staff != null)
        {
            BookService.Unsubscribe(staff, false);
            Console.WriteLine("Staff is unsubscribed.\n");
        }
        else Console.WriteLine("Staff is not unsubscribed. There isn't staff member with this id.\n");
    }
    
    static async Task AsyncBookStore(int bookId, string name, string author, int customerId)
    {
        var order = await BookService.CreateBookOrder(bookId, name, author, customerId);
        await BookService.PlaceOrder(order); 
    }

    static async Task OptionOrderBook()
    {
        try
        {
            Console.WriteLine("Write book order id:");
            int bookId = ParseInputNum();
            Console.WriteLine("Write book name:");
            string name = Console.ReadLine();
            Console.WriteLine("Write book author:");
            string author = Console.ReadLine();
            Console.WriteLine("Write customer id:");
            int customerId = ParseInputNum();

            await AsyncBookStore(bookId, name, author, customerId);

            Console.WriteLine("Order book is placed.\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }
    
    static void PrintMenu()
    {
        Console.WriteLine("1. Subscribe customer");
        Console.WriteLine("2. Subscribe staff");
        Console.WriteLine("3. Unsubscribe customer");
        Console.WriteLine("4. Unsubscribe staff");
        Console.WriteLine("5. Order a book");
        Console.WriteLine("0. Exit\n");
    }
    
    internal static async Task MainFunction()
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

            if (choice == 0) break;
            if (choice == 1) OptionSubscribeCustomer();
            else if (choice == 2) OptionSubscribeStaff();
            else if (choice == 3) OptionUnsubscribeCustomer();
            else if (choice == 4) OptionUnsubscribeStaff();
            else if (choice == 5) await OptionOrderBook();
            else
            {
                Console.WriteLine("Unknown option. Try again.");
                continue;
            }
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}