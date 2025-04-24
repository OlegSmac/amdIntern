namespace project1.Lesson_12_Assignment;

public static class UserService
{
    static List<User> _users = new()
    {
        new User("OlegSmac", "oleansmacinih@gmail.com", "+37379240713", "26542"),
        new User("Nikita", "nikita@gmail.com", "079534453", "42244"),
        new User("Tatiana", "tatiana@gmail.com", "+37379101317", "42245")
    };

    public static User GetUser(Func<User, bool> compare)
    {
        return _users.FirstOrDefault(compare);
    }
    
    public static bool ExistsUser(User inputUser)
    {
        return _users.Any(user =>
            user.Nickname == inputUser.Nickname ||
            user.Email == inputUser.Email ||
            user.PhoneNumber == inputUser.PhoneNumber ||
            user.DeviceId == inputUser.DeviceId
        );
    }

    public static void AddUser(User user)
    {
        _users.Add(user);
    }
}