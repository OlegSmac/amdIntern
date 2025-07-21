using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Requests.Posts.Categories.Commands;
using Vehicles.Application.Requests.Posts.Categories.Queries;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Company, Admin")]
    public async Task<IActionResult> CreateCategory(CreateCategory command)
    {
        await _mediator.Send(command);

        return Ok(true);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var command = new GetAllCategories();
        var response = await _mediator.Send(command);
        var result = response.Select(c => c.Name);

        return Ok(result);
    }

}