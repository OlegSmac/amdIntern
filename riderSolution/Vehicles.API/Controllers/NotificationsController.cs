using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Models.DTOs.Notifications;
using Vehicles.API.Models.Requests.Notifications;
using Vehicles.Application.Notifications.Commands;
using Vehicles.Application.Notifications.Queries;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain.Notifications.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public NotificationsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("getPagedNotifications/{type}")]
    public async Task<IActionResult> GetPagedNotifications([FromRoute] string type, [FromBody] PagedRequest request)
    {
        return type switch
        {
            "Admin" => await GetTypedNotifications<GetNotificationsPaged<AdminNotification>, AdminNotification, AdminNotificationDTO>(request),
            "Company" => await GetTypedNotifications<GetNotificationsPaged<CompanyNotification>, CompanyNotification, CompanyNotificationDTO>(request),
            "User" => await GetTypedNotifications<GetNotificationsPaged<UserNotification>, UserNotification, UserNotificationDTO>(request),
            _ => BadRequest("Unknown notification type")
        };
    }

    [HttpPost("setRead/{id}")]
    public async Task<IActionResult> SetNotificationRead([FromRoute] int id)
    {
        var command = new SetNotificationReadById(id);
        await _mediator.Send(command);

        return Ok(true);
    }

    [HttpPost("sendAdminNotification")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SendAdminNotification([FromBody] AdminNotificationRequest request)
    {
        var command = new SendAdminNotification(request.Company, request.Brand, request.Model, request.Year);
        await _mediator.Send(command);

        return Ok(true);
    }
    
    [HttpPost("setResolved/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SetAdminNotificationResolved([FromRoute] int id)
    {
        var command = new SetAdminNotificationResolvedById(id);
        await _mediator.Send(command);

        return Ok(true);
    }
    
    private async Task<IActionResult> GetTypedNotifications<TCommand, TEntity, TDto>(PagedRequest request)
        where TCommand : IRequest<PaginatedResult<TEntity>>, new()
        where TEntity : class
        where TDto : class
    {
        var command = new TCommand();
        var prop = typeof(TCommand).GetProperty(nameof(PagedRequest));
        if (prop is null) return BadRequest("PagedRequest property is missing on command.");
        prop.SetValue(command, request);

        var response = await _mediator.Send(command);

        var dtoItems = response.Items.Select(item => _mapper.Map<TDto>(item)).ToList();
        var result = new PaginatedResult<TDto>
        {
            Items = dtoItems,
            PageIndex = response.PageIndex,
            PageSize = response.PageSize,
            Total = response.Total
        };

        return Ok(result);
    }

}