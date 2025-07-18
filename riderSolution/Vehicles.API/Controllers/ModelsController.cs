using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Requests.Vehicles.Models.Commands;
using Vehicles.Application.Requests.Vehicles.Models.Queries;
using Vehicles.Application.Requests.Vehicles.Vehicles.Commands;

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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateModel([FromBody] CreateModel request)
    {
        if (request == null) return BadRequest("Request body cannot be null.");

        var modelDto = await _mediator.Send(request);

        return Ok(new { brand = modelDto.Brand, model = modelDto.Model, year = modelDto.Year});
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteModel([FromBody] RemoveModel request)
    {
        if (request == null) return BadRequest("Request body cannot be null.");

        await _mediator.Send(request);
        return NoContent();
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetAllBrands()
    {
        var command = new GetAllBrands();
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }
    
    [HttpGet("models/{brand}")]
    public async Task<IActionResult> GetAllModelsByBrand([FromRoute] string brand)
    {
        var command = new GetAllModelsByBrand(brand);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpGet("years/{brand}/{model}")]
    public async Task<IActionResult> GetAllYearsByBrandAndModel([FromRoute] string brand, [FromRoute] string model)
    {
        var command = new GetAllYearsByBrandAndModel(brand, model);
        var response = await _mediator.Send(command);
        
        return Ok(response);
    }

}