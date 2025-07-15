using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.DTOs.Users;
using Vehicles.API.Extensions;
using Vehicles.API.Services;
using Vehicles.Application.PaginationModels;
using Vehicles.Application.Users.Companies.Queries;
using Vehicles.Domain.Users.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public CompaniesController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }
    
    [HttpPost("getPagedCompanies")]
    public async Task<IActionResult> GetPagedCompanies([FromBody] PagedRequest request)
    {
        var command = new GetPagedCompanies(request.PageIndex, request.PageSize);
        var response = await _mediator.Send(command);
        var result = await response.MapPaginatedResultAsync<Company, CompanyDTO>(async company =>
            await UserService.GetUserByIdAsync(_mediator, _userManager, company.Id) as CompanyDTO
        );
        
        return Ok(result);
    }
}