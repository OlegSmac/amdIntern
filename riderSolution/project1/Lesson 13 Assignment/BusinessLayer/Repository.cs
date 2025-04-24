namespace project1.Lesson_13_Assignment.BusinessLayer;

public class Repository : IRepository
{
    private int currentId = 1;
    private Dictionary<int, KeyValuePair<Speaker, int>> speakers = new Dictionary<int, KeyValuePair<Speaker, int>>();
    
    public int SaveSpeaker(Speaker speaker, int registrationFee)
    {
        if (speaker == null) throw new ArgumentNullException(nameof(speaker), "Speaker cannot be null.");
        
        speakers[currentId] = new KeyValuePair<Speaker, int>(speaker, registrationFee);
        Console.WriteLine($"Speaker '{speaker.FirstName} {speaker.LastName}' saved.");
        
        return currentId++;
    }
}
