namespace project1.Lesson_13_Assignment.BusinessLayer;

public class Repository : IRepository
{
    private int currentId = 1;
    private Dictionary<int, Speaker> speakers = new Dictionary<int, Speaker>();
    
    public int SaveSpeaker(Speaker speaker)
    {
        if (speaker == null) throw new ArgumentNullException(nameof(speaker), "Speaker cannot be null.");
        
        speakers[currentId] = speaker;
        Console.WriteLine($"Speaker '{speaker.FirstName} {speaker.LastName}' saved.");
        
        return currentId++;
    }
}
