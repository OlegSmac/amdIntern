using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.DTOs.Posts;
using Vehicles.Application.Statistics.Queries;
using Vehicles.API.Contracts.DTOs.Statistics;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public StatisticsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("lastYearFavoritesCount/{companyId}")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetLastYearFavoriteCountStatistics([FromRoute] string companyId)
    {
        var command = new GetLastYearFavoriteCountStatistics(companyId);
        var response = await _mediator.Send(command);

        var result = response
            .Select(mc => new MonthCountDTO
            {
                Month = mc.Key,
                Count = mc.Value
            })
            .ToList();
        
        return Ok(result);
    }
    
    [HttpGet("lastYearPostsCount/{companyId}")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetLastYearPostsCountStatistics([FromRoute] string companyId)
    {
        var command = new GetLastYearPostsCountStatistics(companyId);
        var response = await _mediator.Send(command);

        var result = response
            .Select(mc => new MonthCountDTO
            {
                Month = mc.Key,
                Count = mc.Value
            })
            .ToList();
        
        return Ok(result);
    }

    [HttpGet("top3Posts/{companyId}")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetTop3Posts([FromRoute] string companyId)
    {
        var command = new GetTop3Posts(companyId);
        var response = await _mediator.Send(command);
        var result = response.Select(p => new {
            Post = _mapper.Map<PostDTO>(p.Key),
            Count = p.Value
        }).ToList();

        return Ok(result);
    }
    
    [HttpGet("totalCompanyPosts/{companyId}")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetTotalCompanyPostsCount([FromRoute] string companyId)
    {
        var command = new GetTotalCompanyPostsCount(companyId);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
    
    [HttpGet("totalCompanyFavoriteCount/{companyId}")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetTotalCompanyFavoriteCount([FromRoute] string companyId)
    {
        var command = new GetTotalCompanyFavoriteCount(companyId);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
}