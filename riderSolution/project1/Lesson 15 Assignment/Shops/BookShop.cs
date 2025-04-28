using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Shops;

public class BookShop : Shop
{
    public override async Task Notify(ISubscriber subscriber, string message)
    {
        await subscriber.ReceiveMessage("Book shop notification: " + message);
    }
}