using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.Requests.Users;
using Vehicles.Application.Users.Users.Commands;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public SubscriptionsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest request)
    {
        var command = new SubscribeToCompany(request.UserId, request.CompanyId);
        await _mediator.Send(command);
        
        return Ok(true);
    }

    [HttpDelete]
    public async Task<IActionResult> Unsubscribe([FromBody] SubscriptionRequest request)
    {
        var command = new UnsubscribeFromCompany(request.UserId, request.CompanyId);
        await _mediator.Send(command);
        
        return Ok(true);
    }
    
}