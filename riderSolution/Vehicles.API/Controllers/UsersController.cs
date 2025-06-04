using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Application.Users.Admins.Commands;
using Vehicles.Application.Users.Admins.Queries;
using Vehicles.Application.Users.Companies.Commands;
using Vehicles.Application.Users.Companies.Queries;
using Vehicles.Application.Users.RegularUsers.Commands;
using Vehicles.Application.Users.RegularUsers.Queries;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("regularUsers")]
    public async Task<IActionResult> CreateRegularUser(RegularUser user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new CreateUser(user.Name, user.Email, user.Password);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpPut("regularUsers")]
    public async Task<IActionResult> UpdateRegularUser(RegularUser user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new UpdateUser(user.Id, user.Name, user.Email, user.Password);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpGet("regularUsers/{id}")]
    public async Task<IActionResult> GetRegularUserById([FromRoute] int id)
    {
        var command = new GetUserById(id);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpGet("regularUsers")]
    public async Task<IActionResult> GetAllRegularUsers()
    {
        var command = new GetAllUsers();
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpDelete("regularUsers/{id}")]
    public async Task<IActionResult> DeleteRegularUserById([FromRoute] int id)
    {
        var command = new RemoveUser(id);
        await _mediator.Send(command);
        
        return Ok(true);
    }
    
    [HttpPost("companies")]
    public async Task<IActionResult> CreateCompany(Company company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new CreateCompany(company.Name, company.Email, company.Password, company.Description);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpPut("companies")]
    public async Task<IActionResult> UpdateCompany(Company company)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new UpdateCompany(company.Id, company.Name, company.Email, company.Password, company.Description);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpGet("companies/{id}")]
    public async Task<IActionResult> GetComapanyById([FromRoute] int id)
    {
        var command = new GetCompanyById(id);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpGet("companies")]
    public async Task<IActionResult> GetAllCompanies()
    {
        var command = new GetAllCompanies();
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpDelete("companies/{id}")]
    public async Task<IActionResult> DeleteCompanyById([FromRoute] int id)
    {
        var command = new RemoveCompany(id);
        await _mediator.Send(command);
        
        return Ok(true);
    }
    
    [HttpPost("admins")]
    public async Task<IActionResult> CreateAdmin(Admin admin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new CreateAdmin(admin.Name, admin.Email, admin.Password);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpPut("admins")]
    public async Task<IActionResult> UpdateAdmin(Admin admin)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new UpdateAdmin(admin.Id, admin.Name, admin.Email, admin.Password);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpGet("admins/{id}")]
    public async Task<IActionResult> GetAdminById([FromRoute] int id)
    {
        var command = new GetAdminById(id);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

    [HttpGet("admins")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var command = new GetAllAdmins();
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpDelete("admins/{id}")]
    public async Task<IActionResult> DeleteAdminById([FromRoute] int id)
    {
        var command = new RemoveAdmin(id);
        await _mediator.Send(command);
        
        return Ok(true);
    }
}