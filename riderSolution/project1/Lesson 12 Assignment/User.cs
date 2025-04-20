namespace project1.Lesson_12_Assignment;

public class User
{
    public string Nickname { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public string DeviceId { get; }

    public User(string nickname, string email, string phoneNumber, string deviceId)
    {
        Nickname = nickname;
        Email = email;
        PhoneNumber = phoneNumber;
        DeviceId = deviceId;
    }
}
