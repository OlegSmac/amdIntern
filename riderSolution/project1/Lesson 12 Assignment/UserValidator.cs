namespace project1.Lesson_12_Assignment;

public static class UserValidator
{
    public static bool CanUserBeAdded(User inputUser)
    {
        return UserService.ExistsUser(inputUser);
    }
}