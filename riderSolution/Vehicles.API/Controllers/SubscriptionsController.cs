using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.Requests.Users;
using Vehicles.Application.Requests.Users.Users.Commands;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest request)
    {
        var command = new SubscribeToCompany(request.UserId, request.CompanyId);
        await _mediator.Send(command);
        
        return Ok(true);
    }

    [HttpDelete]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Unsubscribe([FromBody] SubscriptionRequest request)
    {
        var command = new UnsubscribeFromCompany(request.UserId, request.CompanyId);
        await _mediator.Send(command);
        
        return Ok(true);
    }
    
}