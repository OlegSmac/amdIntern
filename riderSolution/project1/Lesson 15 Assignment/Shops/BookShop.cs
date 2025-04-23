using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Shops;

public class BookShop : Shop
{
    public override void Notify(ISubscriber subscriber, string message)
    {
        subscriber.ReceiveMessage("Book shop notification: " + message);
    }
}