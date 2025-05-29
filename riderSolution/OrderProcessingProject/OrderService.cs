using System.Text.Json;
using project1.Lesson_15_Assignment.Orders;

namespace OrderProcessingProject;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

public class OrderService
{
    private static readonly string _ordersFilePath = "/home/thinkpad/Amdaris/amdIntern/riderSolution/project1/Lesson 15 Assignment/BookOrders.json";
    private static Timer orderTimer;

    public static async Task<List<BookOrder>> ReadOrdersFromFileAsync()
    {
        try
        {
            string jsonString = await File.ReadAllTextAsync(_ordersFilePath);
            List<BookOrder> orders = JsonSerializer.Deserialize<List<BookOrder>>(jsonString);

            return orders ?? new List<BookOrder>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error reading orders: {e.Message}");
            return new List<BookOrder>();
        }
    }


    private static async Task ProcessOrderAsync(BookOrder order)
    {
        Console.WriteLine($"Processing Order ID: {order.Id}, Book: {order.Name}, Author: {order.Author}, Customer ID: {order.CustomerId}");
    }

    private static async Task RemoveProcessedOrdersAsync()
    {
        try
        {
            await File.WriteAllTextAsync(_ordersFilePath, "{}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to file: {ex.Message}");
        }
    }
    
    public static async Task ProcessOrdersAsync()
    {
        List<BookOrder> orders = await ReadOrdersFromFileAsync();
        await RemoveProcessedOrdersAsync();
        
        foreach (var order in orders)
        {
            await ProcessOrderAsync(order);
        }
    }

    public static void StartOrderProcessing()
    {
        orderTimer = new Timer(60000);
        orderTimer.Elapsed += async (sender, e) => await ProcessOrdersAsync();
        orderTimer.Start();

        Console.WriteLine("Order processing started.");
    }
}
