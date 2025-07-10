using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using DomainVehicle = Vehicles.Domain.VehicleTypes.Models.Vehicle;

namespace Vehicles.Application.Posts.Posts.Queries;

public record GetPostById(int Id) : IRequest<Post>;

public class GetPostByIdHandler : IRequestHandler<GetPostById, Post>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetPostByIdHandler> _logger;

    public GetPostByIdHandler(IUnitOfWork unitOfWork, ILogger<GetPostByIdHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Post> Handle(GetPostById request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetPostById was called");
        ArgumentNullException.ThrowIfNull(request);
        
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync<Post>(request.Id);
        Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(post.CompanyId);
        DomainVehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<DomainVehicle>(post.VehicleId);
        List<PostImage> images = await _unitOfWork.ImageRepository.GetByPostIdAsync(request.Id);
        
        if (company is null || vehicle is null) throw new NullReferenceException("There isn't a Company or a Vehicle or both of them.");
        
        post.Company = company;
        post.Vehicle = vehicle;
        post.Images = images;

        return post;
    }
}