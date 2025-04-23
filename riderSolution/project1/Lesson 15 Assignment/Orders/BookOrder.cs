using project1.Lesson_15_Assignment.Enums;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Orders;

public class BookOrder : Order
{
    public string Author { get; set; }

    public BookOrder(int id, string name, string author, ISubscriber customer) : base(id, name, customer, OrderStatus.Placed)
    {
        Author = author;
    }
    
}