using System.Text.Json.Serialization;
using project1.Lesson_15_Assignment.Enums;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Orders;

public abstract class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }

    public Order()
    {
        
    }
    
    public Order(int id, string name, int customerId, OrderStatus status)
    {
        Id = id;
        Name = name;
        CustomerId = customerId;
        Status = status;
    }
}