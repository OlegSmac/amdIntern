using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vehicles.Application.Vehicles.Vehicles.Queries;
using Vehicles.Domain.VehicleTypes.Models;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Models.DTOs.Vehicles;
using Vehicles.API.Models.Factories;
using Vehicles.API.Models.Requests.Vehicles;
using Vehicles.API.Models.DTOs.Vehicles;
using Vehicles.Application.Vehicles.Vehicle.Queries;
using Vehicles.Application.Vehicles.Vehicles.Commands;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public VehiclesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("cars")]
    public async Task<IActionResult> CreateVehicle(CreateVehicleRequest vehicleRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = VehicleCommandFactory.CreateCommand(vehicleRequest, _mapper);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<VehicleDTO>(response);
        
        return Ok(result);
    }

    [HttpPut("cars")]
    public async Task<IActionResult> UpdateVehicle(UpdateVehicleRequest vehicleRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = VehicleCommandFactory.UpdateCommand(vehicleRequest, _mapper);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<VehicleDTO>(response);
        
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllVehicles()
    {
        var command = new GetAllVehicles();
        var response = await _mediator.Send(command);

        var result = response
            .Select(vehicle =>
            {
                var dtoType = VehicleType.GetDtoType(vehicle);
                return _mapper.Map(vehicle, vehicle.GetType(), dtoType);
            })
            .ToList();

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetVehicleById([FromRoute] int id)
    {
        var command = new GetVehicleById(id);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<VehicleDTO>(response);
        
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