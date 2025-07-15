using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Contracts.DTOs.Posts;
using Vehicles.API.Contracts.Requests.Posts;
using Vehicles.API.Extensions;
using Vehicles.API.Models.Factories;
using Vehicles.API.Services;
using Vehicles.Application.PaginationModels;
using Vehicles.Application.Posts.Posts.Commands;
using Vehicles.Application.Posts.Posts.Queries;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public PostsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    
    [HttpPost]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var vehicleCommand = VehicleCommandFactory.CreateCommand(request.Vehicle, _mapper);
        var vehicleResponse = await _mediator.Send(vehicleCommand);
        request.VehicleId = vehicleResponse.Id;
        
        var entity = _mapper.Map<Post>(request);
        entity.Categories = await CategoryService.CreateCategoryList(request.Categories, _mediator);
        
        var command = new CreatePost(entity);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<PostDTO>(response);
        
        return Ok(result);
    }

    [HttpPut]
    [Authorize(Roles = "Company, Admin")]
    public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request)
    {
        var vehicleCommand = VehicleCommandFactory.UpdateCommand(request.Vehicle, _mapper);
        await _mediator.Send(vehicleCommand);
        
        var entity = _mapper.Map<Post>(request);
        entity.Categories = await CategoryService.CreateCategoryList(request.Categories, _mediator);
        
        var command = new UpdatePost(entity);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<PostDTO>(response);
        
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById([FromRoute] int id)
    {
        var command = new GetPostById(id);
        var response = await _mediator.Send(command);
        var result = _mapper.Map<PostDTO>(response);
        
        return Ok(result);
    }

    [HttpPost("paginated-search")]
    public async Task<IActionResult> GetPagedPosts(PagedRequest pagedRequest)
    {
        var command = new GetPostsPaged { PagedRequest = pagedRequest};
        var response = await _mediator.Send(command);
        var result = response.MapPaginatedResult<Post, PostDTO>(_mapper);
        
        return Ok(result);
    }
    
    [HttpPost("{id}")]
    [Authorize(Roles = "Company, Admin")]
    public async Task<IActionResult> HidePost([FromRoute] int id, [FromBody] bool isHidden)
    {
        var command = new HideOrUnhidePost(id, isHidden);
        await _mediator.Send(command);
        
        return Ok(true);
    }

    [HttpGet("favoriteCount/{id}")]
    public async Task<IActionResult> GetPostFavoriteCount([FromRoute] int id)
    {
        var command = new GetPostFavoriteCount(id);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Company, Admin")]
    public async Task<IActionResult> DeletePost([FromRoute] int id)
    {
        var command = new RemovePost(id);
        await _mediator.Send(command);

        return Ok(true);
    }
}