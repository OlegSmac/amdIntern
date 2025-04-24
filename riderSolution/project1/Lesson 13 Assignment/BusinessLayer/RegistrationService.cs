namespace project1.Lesson_13_Assignment.BusinessLayer;

public class RegistrationService
{
    IRepository repository = new Repository();
    private int CalculateRegistrationFee(int? experience)
    {
        if (experience == null || experience <= 1) return 500;
        if (experience <= 3) return 250;
        if (experience <= 5) return 100;
        if (experience <= 9) return 50;
        
        return 0;
    }
    
    /// <summary>
    /// Register a speaker
    /// </summary>
    /// <returns>speakerID</returns>
    public int? Register(Speaker speaker)
    {
        try
        {
            SpeakerValidator.IsSpeakerValid(speaker);
        }
        catch (Exception e)
        {
            Console.WriteLine("Invalid speaker: " + e.Message);
            return null;
        }

        //if we got this far, the speaker is approved
        //let's go ahead and register him/her now.
        //First, let's calculate the registration fee. 
        //More experienced speakers pay a lower fee.
        //Now, save the speaker and sessions to the db.
        try
        {
            return repository.SaveSpeaker(speaker, CalculateRegistrationFee(speaker.Experience));
        }
        catch (Exception e)
        {
            //in case the db call fails
            throw new Exception("Error saving speaker", e);
        }
    }
}