using System.Reflection.Metadata;
using project1.Lesson_12_Assignment;
using project1.Lesson_15_Assignment.Orders;
using project1.Lesson_15_Assignment.Shops;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment;

public class Lesson15Main
{
    private static Shop _bookShop = new BookShop();

    private static List<Users.Customer> _customers = new List<Users.Customer>()
    {
        new Users.Customer(1, "Oleg"),
        new Users.Customer(2, "Nikita"),
    };
    
    private static List<Staff> _staff = new List<Staff>()
    {
        new Staff(1, "Goga"),
        new Staff(2, "Petr"),
    };

    private static int ParseInputNum()
    {
        string input = Console.ReadLine();
        int num;
    
        while (!int.TryParse(input, out num))
        {
            Console.WriteLine("Input must be a number.");
            input = Console.ReadLine();
        }

        return num;
    }

    private static void OptionSubscribeCustomer()
    {
        Console.WriteLine("Write customer id:");
        int id = ParseInputNum();
        
        var customer = _customers.Find(customer => customer.Id == id);
        if (customer != null)
        {
            _bookShop.Subscribe(customer, true);
            Console.WriteLine("Customer is subscribed.\n");
        }
        else Console.WriteLine("Customer is not subscribed. There isn't customer with this id.\n");
    }
    
    private static void OptionSubscribeStaff()
    {
        Console.WriteLine("Write staff id:");
        int id = ParseInputNum();

        var staff = _staff.Find(staff => staff.Id == id);
        if (staff != null)
        {
            _bookShop.Subscribe(staff, false);
            Console.WriteLine("Staff is subscribed.\n");
        }
        else Console.WriteLine("Staff is not subscribed. There isn't staff member with this id.\n");
    }
    
    private static void OptionUnsubscribeCustomer()
    {
        Console.WriteLine("Write customer id:");
        int id = ParseInputNum();

        var customer = _customers.Find(customer => customer.Id == id);
        if (customer != null)
        {
            _bookShop.Unsubscribe(customer, true);
            Console.WriteLine("Customer is unsubscribed.\n");
        }
        else Console.WriteLine("Customer is not unsubscribed. There isn't customer with this id.\n");
    }

    private static void OptionUnsubscribeStaff()
    {
        Console.WriteLine("Write staff id:");
        int id = ParseInputNum();

        var staff = _staff.Find(staff => staff.Id == id);
        if (staff != null)
        {
            _bookShop.Unsubscribe(staff, false);
            Console.WriteLine("Staff is unsubscribed.\n");
        }
        else Console.WriteLine("Staff is not unsubscribed. There isn't staff member with this id.\n");
    }

    private static void OptionOrderBook()
    {
        Console.WriteLine("Write order book id:");
        int bookId = ParseInputNum();
        Console.WriteLine("Write book name:");
        string name = Console.ReadLine();
        Console.WriteLine("Write book author:");
        string author = Console.ReadLine();
        Console.WriteLine("Write customer id:");
        int customerId = ParseInputNum();
        
        _bookShop.PlaceOrder(new BookOrder(bookId, name, author, _customers.Find(c => c is Users.Customer customer && customer.Id == customerId)));
        Console.WriteLine("Order book is placed.\n");
    }

    private static void OptionProcessOption()
    {
        _bookShop.ProcessNextOrder();
        Console.WriteLine();
    }
    
    static void PrintMenu()
    {
        Console.WriteLine("1. Subscribe customer");
        Console.WriteLine("2. Subscribe staff");
        Console.WriteLine("3. Unsubscribe customer");
        Console.WriteLine("4. Unsubscribe staff");
        Console.WriteLine("5. Order a book");
        Console.WriteLine("6. Process order");
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

            if (choice == 0) break;
            if (choice == 1) OptionSubscribeCustomer();
            else if (choice == 2) OptionSubscribeStaff();
            else if (choice == 3) OptionUnsubscribeCustomer();
            else if (choice == 4) OptionUnsubscribeStaff();
            else if (choice == 5) OptionOrderBook();
            else if (choice == 6) OptionProcessOption();
            else
            {
                Console.WriteLine("Unknown option. Try again.");
                continue;
            }

        }
    }
}