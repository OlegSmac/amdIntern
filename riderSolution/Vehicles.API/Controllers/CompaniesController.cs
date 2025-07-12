using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.DTOs.Users;
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
        
        var dtoItems = new List<CompanyDTO>();
        foreach (var company in response.Items)
        {
            var dto = await UserService.GetUserByIdAsync(_mediator, _userManager, company.Id);
            if (dto != null) dtoItems.Add((CompanyDTO)dto);
        }

        var result = new PaginatedResult<CompanyDTO>
        {
            Items = dtoItems,
            PageIndex = response.PageIndex,
            PageSize = response.PageSize,
            Total = response.Total
        };
        
        return Ok(result);
    }
}