using MediatR;
using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Vehicles.Vehicles.Queries;
using Vehicles.Domain.VehicleTypes.Models;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Factories;
using Vehicles.API.Requests;
using Vehicles.Application.Vehicles.Cars.Commands;
using Vehicles.Application.Vehicles.Motorcycles.Commands;
using Vehicles.Application.Vehicles.Trucks.Commands;
using Vehicles.Application.Vehicles.Vehicle.Queries;
using Vehicles.Application.Vehicles.Vehicles.Commands;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;

    public VehiclesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("cars")]
    public async Task<IActionResult> CreateVehicle(VehicleRequest vehicleRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = VehicleCommandFactory.CreateCommand(vehicleRequest);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpPut("cars")]
    public async Task<IActionResult> UpdateVehicle(VehicleRequest vehicleRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = VehicleCommandFactory.UpdateCommand(vehicleRequest);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var command = new GetAllVehicles();
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleById([FromRoute] int id)
    {
        var command = new GetVehicleById(id);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
    {
        var command = new RemoveVehicle(id);
        await _mediator.Send(command);
        
        return Ok(true);
    }
}