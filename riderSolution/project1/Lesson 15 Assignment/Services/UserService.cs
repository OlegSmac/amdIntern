using project1.Lesson_15_Assignment.Users;

namespace project1.Lesson_15_Assignment.Services;

public static class UserService
{
    private static List<Users.Customer> _customers = new()
    {
        new Users.Customer(1, "Oleg"),
        new Users.Customer(2, "Nikita"),
    };
    
    private static List<Staff> _staff = new()
    {
        new Staff(1, "Goga"),
        new Staff(2, "Petr"),
    };

    public static Users.Customer GetCustomer(int customerId)
    {
        return _customers.Find(customer => customer.Id == customerId);
    }
    
    public static Staff GetStaff(int staffId)
    {
        return _staff.Find(staff => staff.Id == staffId);
    }
}