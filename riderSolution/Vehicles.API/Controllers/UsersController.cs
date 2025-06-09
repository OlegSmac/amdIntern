using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.DTOs.Responses;
using Vehicles.API.Models.Requests.Users;
using Vehicles.Application.Users.Admins.Commands;
using Vehicles.Application.Users.Admins.Queries;
using Vehicles.Application.Users.Companies.Commands;
using Vehicles.Application.Users.Companies.Queries;
using Vehicles.Application.Users.Users.Commands;
using Vehicles.Application.Users.Users.Queries;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(CreateUserRequest userRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var user = _mapper.Map<User>(userRequest);
        var command = new CreateUser(user.Name, user.Email, user.Password);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<UserDTO>(response);
            
        return Ok(result);
    }

    [HttpPut("users")]
    public async Task<IActionResult> UpdateUser(UpdateUser userRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var command = new UpdateUser(userRequest.Id, userRequest.Name, userRequest.Email, userRequest.Password);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<UserDTO>(response);
        
        return Ok(result);
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var command = new GetUserById(id);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<UserDTO>(response);
        
        return Ok(result);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var command = new GetAllUsers();
        var response = await _mediator.Send(command);
        var result = _mapper.Map<List<UserDTO>>(response);
        
        return Ok(result);
    }

    [HttpDelete("users/{id}")]
    public async Task<IActionResult> DeleteUserById([FromRoute] int id)
    {
        var command = new RemoveUser(id);
        await _mediator.Send(command);
        
        return Ok(true);
    }
    
    [HttpPost("companies")]
    public async Task<IActionResult> CreateCompany(CreateCompanyRequest companyRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var company = _mapper.Map<Company>(companyRequest);
        var command = new CreateCompany(company.Name, company.Email, company.Password, company.Description);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<CompanyDTO>(response);
        
        return Ok(result);
    }

    [HttpPut("companies")]
    public async Task<IActionResult> UpdateCompany(UpdateCompany companyRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var company = _mapper.Map<Company>(companyRequest);
        var command = new UpdateCompany(company.Id, company.Name, company.Email, company.Password, company.Description);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<CompanyDTO>(response);
        
        return Ok(result);
    }
    
    [HttpGet("companies/{id}")]
    public async Task<IActionResult> GetComapanyById([FromRoute] int id)
    {
        var command = new GetCompanyById(id);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<CompanyDTO>(response);
        
        return Ok(result);
    }

    [HttpGet("companies")]
    public async Task<IActionResult> GetAllCompanies()
    {
        var command = new GetAllCompanies();
        var response = await _mediator.Send(command);
        var result = _mapper.Map<List<CompanyDTO>>(response);
        
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
    public async Task<IActionResult> CreateAdmin(CreateAdminRequest adminRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var admin = _mapper.Map<Admin>(adminRequest);
        var command = new CreateAdmin(admin.Name, admin.Email, admin.Password);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<AdminDTO>(response);
        
        return Ok(result);
    }

    [HttpPut("admins")]
    public async Task<IActionResult> UpdateAdmin(UpdateAdminRequest adminRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var admin = _mapper.Map<Admin>(adminRequest);
        var command = new UpdateAdmin(admin.Id, admin.Name, admin.Email, admin.Password);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<AdminDTO>(response);
        
        return Ok(result);
    }
    
    [HttpGet("admins/{id}")]
    public async Task<IActionResult> GetAdminById([FromRoute] int id)
    {
        var command = new GetAdminById(id);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<AdminDTO>(response);
        
        return Ok(result);
    }

    [HttpGet("admins")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var command = new GetAllAdmins();
        var response = await _mediator.Send(command);
        var result = _mapper.Map<List<AdminDTO>>(response);
        
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