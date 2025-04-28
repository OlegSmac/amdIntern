using System.Text.Json;
using project1.Lesson_15_Assignment.Enums;
using project1.Lesson_15_Assignment.Orders;
using project1.Lesson_15_Assignment.Services;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Shops;

public abstract class Shop
{
    private List<ISubscriber> Customers = new();
    private List<ISubscriber> Staff = new();
    
    private static readonly string _ordersFilePath = "/home/thinkpad/Amdaris/amdIntern/riderSolution/project1/Lesson 15 Assignment/BookOrders.json";

    public void Subscribe(ISubscriber subscriber, bool isCustomer)
    {
        if (isCustomer) Customers.Add(subscriber);
        else Staff.Add(subscriber);
    }

    public void Unsubscribe(ISubscriber subscriber, bool isCustomer)
    {
        if (isCustomer) Customers.Remove(subscriber);
        else Staff.Remove(subscriber);
    }
    
    private bool IsCustomerSubscribed(ISubscriber customer)
    {
        return Customers.Contains(customer);
    }

    private async Task<List<BookOrder>> LoadBookOrdersAsync()
    {
        try 
        {
            string existingOrdersJson = await File.ReadAllTextAsync(_ordersFilePath);
            return string.IsNullOrWhiteSpace(existingOrdersJson) ? new List<BookOrder>() : JsonSerializer.Deserialize<List<BookOrder>>(existingOrdersJson);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading orders: {e.Message}");
            return new List<BookOrder>();
        }
    }

    private async Task SaveBookOrderToFileAsync(BookOrder order)
    {
        List<BookOrder> ordersInFile = await LoadBookOrdersAsync();
        ordersInFile.Add(order);
        
        string updatedOrdersJson = JsonSerializer.Serialize(ordersInFile);
        await File.WriteAllTextAsync(_ordersFilePath, updatedOrdersJson);
    }

    public async Task PlaceOrder(Order order)
    {
        await SaveBookOrderToFileAsync((BookOrder)order);
        
        if (IsCustomerSubscribed(UserService.GetCustomer(order.CustomerId)))
        {
            Notify(UserService.GetCustomer(order.CustomerId), $"Order #{order.Id} has been placed.");
        }
        
        NotifyAll(Staff, $"Order #{order.Id} has been placed.");
    }

    private void ChangeOrderStatus(Order order, OrderStatus status)
    {
        order.Status = status;
    }

    public virtual async Task Notify(ISubscriber subscriber, string message)
    {
        await subscriber.ReceiveMessage(message);
    }
    
    public async Task NotifyAll(List<ISubscriber> subscribers, string message)
    {
        var tasks = subscribers.Select(subscriber => Task.Run(() => Notify(subscriber, message)));
        await Task.WhenAll(tasks);
    }
}