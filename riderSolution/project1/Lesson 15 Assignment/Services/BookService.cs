using project1.Lesson_15_Assignment.Orders;
using project1.Lesson_15_Assignment.Shops;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Services;

public static class BookService
{
    private static Shop _bookShop = new BookShop();

    public static void Subscribe(ISubscriber subscriber, bool isCustomer)
    {
        _bookShop.Subscribe(subscriber, isCustomer);
    }
    
    public static void Unsubscribe(ISubscriber subscriber, bool isCustomer)
    {
        _bookShop.Unsubscribe(subscriber, isCustomer);
    }
    
    public static Task<BookOrder> CreateBookOrder(int id, string name, string author, int customerId)
    {
        var order = new BookOrder(id, name, author, customerId);
        return Task.FromResult(order);
    }

    public static async Task PlaceOrder(BookOrder order)
    {
        await _bookShop.PlaceOrder(order);
    }
}