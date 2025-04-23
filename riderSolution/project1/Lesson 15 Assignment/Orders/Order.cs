using project1.Lesson_15_Assignment.Enums;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Orders;

public abstract class Order
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ISubscriber Customer { get; set; }
    public OrderStatus Status { get; set; }

    public Order(int id, string name, ISubscriber customer, OrderStatus status)
    {
        Id = id;
        Name = name;
        Customer = customer;
        Status = status;
    }
}