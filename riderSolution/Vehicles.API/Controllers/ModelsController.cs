using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Vehicles.Vehicles.Commands;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/models")]
public class ModelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ModelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateModel([FromBody] CreateModel request)
    {
        if (request == null)
            return BadRequest("Request body cannot be null.");

        var modelDto = await _mediator.Send(request);

        return CreatedAtAction(nameof(CreateModel), new { brand = modelDto.Brand, model = modelDto.Model, year = modelDto.Year }, modelDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteModel([FromBody] RemoveModel request)
    {
        if (request == null)
            return BadRequest("Request body cannot be null.");

        await _mediator.Send(request);
        return NoContent();
    }

}