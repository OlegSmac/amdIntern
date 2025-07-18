using System.Net.Mail;
using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Requests.Emails.Commands;

public record SendMessageToEmail(string From, string To, string Subject, string Body): IRequest;

public class SendMessageToEmailHandler : IRequestHandler<SendMessageToEmail>
{
    private readonly ILogger<SendMessageToEmailHandler> _logger;

    public SendMessageToEmailHandler(ILogger<SendMessageToEmailHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(SendMessageToEmail request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SendMessageToEmail was called");
        ArgumentNullException.ThrowIfNull(request);
        
        using var mail = new MailMessage(
            from: request.From,
            to: request.To,
            subject: request.Subject,
            body: request.Body);

        using var smtp = new SmtpClient("localhost", 25);
        await smtp.SendMailAsync(mail);
    }
}