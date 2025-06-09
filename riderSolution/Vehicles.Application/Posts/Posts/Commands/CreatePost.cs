using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record CreatePost(string Title, string Body, DateTime Date, int CompanyId, int VehicleId) : IRequest<Post>;

public class CreatePostHandler : IRequestHandler<CreatePost, Post>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePostHandler> _logger;

    public CreatePostHandler(IUnitOfWork unitOfWork, ILogger<CreatePostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Post> CreatePostAsync(CreatePost request, Company company,  Vehicle vehicle)
    {
        return new Post()
        {
            Title = request.Title,
            Body = request.Body,
            Date = request.Date,
            IsHidden = false,
            Views = 0,
            Company = company,
            Vehicle = vehicle
        };
    }

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreatePost was called");
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.CompanyId);
            Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.VehicleId);
        
            if (company is null || vehicle is null) throw new NullReferenceException("Both Company and Vehicle are required.");

            Post post = await CreatePostAsync(request, company, vehicle);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.CreateAsync(post);
                await _unitOfWork.SaveAsync();
            });

            return post;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }
}