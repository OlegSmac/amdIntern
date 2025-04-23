using project1.Lesson_15_Assignment.Enums;
using project1.Lesson_15_Assignment.Orders;
using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Shops;

public abstract class Shop
{
    private List<ISubscriber> Customers = new List<ISubscriber>();
    private List<ISubscriber> Staff = new List<ISubscriber>();
    private Queue<Order> Orders = new Queue<Order>();

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

    public void PlaceOrder(Order order)
    {
        Orders.Enqueue(order);
        
        if (IsCustomerSubscribed(order.Customer))
        {
            Notify(order.Customer, $"Order #{order.Id} has been placed.");
        }
        
        NotifyAll(Staff, $"Order #{order.Id} has been placed.");
    }

    private void ChangeOrderStatus(Order order, OrderStatus status)
    {
        order.Status = status;
    }

    private bool IsCustomerSubscribed(ISubscriber customer)
    {
        return Customers.Contains(customer);
    }

    public void ProcessNextOrder()
    {
        if (Orders.Count == 0)
        {
            Console.WriteLine("No orders to process.");
            return;
        }

        var order = Orders.Dequeue();
        ChangeOrderStatus(order, OrderStatus.ReadyForShipping);
        
        if (IsCustomerSubscribed(order.Customer))
        {
            Notify(order.Customer, $"Order '{order.Name}' is ready for shipping.");
        }
        NotifyAll(Staff, $"Order #{order.Id} ('{order.Name}') is ready for shipping.");
        
    }

    public void NotifyAll(List<ISubscriber> subscribers, string message)
    {
        subscribers.ForEach(subscriber => Notify(subscriber, message));
    }

    public virtual void Notify(ISubscriber subscriber, string message)
    {
        subscriber.ReceiveMessage(message);
    }
}