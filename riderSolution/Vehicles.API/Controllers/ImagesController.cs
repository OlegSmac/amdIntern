using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vehicles.API.Services;

namespace Vehicles.API.Controllers;

[ApiController]
[Route("api/images")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly MinioService _minioService;

    public ImagesController(IMediator mediator, IMapper mapper, MinioService minioService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _minioService = minioService;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImages([FromForm] List<IFormFile> files)
    {
        var urls = new List<string>();
        foreach (var file in files)
        {
            var path = await _minioService.UploadFileAsync(file, "posts");
            urls.Add(path);
        }

        return Ok(urls);
    }

}