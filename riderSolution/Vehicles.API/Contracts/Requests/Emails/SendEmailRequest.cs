using System.ComponentModel.DataAnnotations;

namespace Vehicles.API.Contracts.Requests.Emails;

public class SendEmailRequest
{
    [EmailAddress]
    public string From { get; set; }
    
    [EmailAddress]
    public string To { get; set; }
    
    public string Subject { get; set; }
    
    public string Body { get; set; }
}