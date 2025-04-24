using project1.Lesson_13_Assignment.BusinessLayer.Exceptions;

namespace project1.Lesson_13_Assignment.BusinessLayer;

public static class SpeakerValidator
{
    public const int EXPERIENCE_YEARS = 10;
    public const int CERTIFICATE_COUNT = 3;
    
    private static bool DoesSpeakerMeetsExperienceRequirement(Speaker speaker)
    {
        return speaker.Experience > EXPERIENCE_YEARS;
    }
    
    private static bool DoesSpeakerMeetsCertificateRequirement(Speaker speaker)
    {
        return speaker.Certifications.Count() > CERTIFICATE_COUNT;
    }
    
    private static bool DoesSpeakerHavePreferredEmployer(Speaker speaker)
    {
        return Speaker.PreferredEmployers.Contains(speaker.Employer);
    }
    
    private static bool DoesSpeakerHaveOldEmailDomain(Speaker speaker)
    {
        return Speaker.UnfavorableDomains.Contains(speaker.Email.Split('@').Last());
    }
    
    private static bool DoesSpeakerUseOldBrowser(Speaker speaker)
    {
        return (speaker.Browser.Name == WebBrowser.BrowserName.InternetExplorer && speaker.Browser.MajorVersion < 9);
    }
    
    private static bool DoesSpeakerMeetsRequirements(Speaker speaker)
    {
        return DoesSpeakerMeetsExperienceRequirement(speaker) ||
               speaker.HasBlog ||
               DoesSpeakerMeetsCertificateRequirement(speaker) ||
               DoesSpeakerHavePreferredEmployer(speaker) ||
               !DoesSpeakerHaveOldEmailDomain(speaker) &&
               !DoesSpeakerUseOldBrowser(speaker);
    }

    private static bool IsAnyApprovedSession(Speaker speaker)
    {
        return speaker.Sessions.Any(session =>
            !Speaker.OldTechnologies.Any(oldTechnology =>
                session.Title.Contains(oldTechnology) || session.Description.Contains(oldTechnology)));
    }
    
    public static bool IsSpeakerValid(Speaker speaker)
    {
        if (string.IsNullOrWhiteSpace(speaker.FirstName)) throw new ArgumentNullException("First Name is required");
        if (string.IsNullOrWhiteSpace(speaker.LastName)) throw new ArgumentNullException("Last name is required.");
        if (string.IsNullOrWhiteSpace(speaker.Email)) throw new ArgumentNullException("Email is required.");
        if (speaker.Sessions.Count() == 0) throw new ArgumentException("Can't register speaker with no sessions to present.");
        if (!DoesSpeakerMeetsRequirements(speaker)) throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our arbitrary and capricious standards.");
        if (IsAnyApprovedSession(speaker)) throw new NoSessionsApprovedException("No sessions approved.");

        return true;
    }
}