using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Models.Requests.Emails;
using Vehicles.Application.Emails.Commands;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/emails")]
public class EmailsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public EmailsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        var command = new SendMessageToEmail(request.From, request.To, request.Subject, request.Body);
        await _mediator.Send(command);
        
        return Ok(true);
    }
}