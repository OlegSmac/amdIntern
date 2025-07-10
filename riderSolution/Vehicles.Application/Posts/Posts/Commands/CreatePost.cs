using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Notifications.Models;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record CreatePost(Post Post) : IRequest<Post>;

public class CreatePostHandler : IRequestHandler<CreatePost, Post>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreatePostHandler> _logger;

    public CreatePostHandler(IUnitOfWork unitOfWork, ILogger<CreatePostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task<Post> CreatePostAsync(CreatePost request, Company company, Vehicle vehicle)
    {
        request.Post.Company = company;
        request.Post.Vehicle = vehicle;
        request.Post.Categories = request.Post.Categories;
        
        return request.Post;
    }

    public async Task<Post> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreatePost was called");
        ArgumentNullException.ThrowIfNull(request);
        
        Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Post.CompanyId);
        Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Vehicle>(request.Post.VehicleId);
    
        if (company is null || vehicle is null) throw new NullReferenceException("Both Company and Vehicle are required.");

        Post post = await CreatePostAsync(request, company, vehicle);
        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.PostRepository.Add<Post>(post);
            await _unitOfWork.SaveAsync();

            //Notifications for subscribers
            var subscriberUserIds = await _unitOfWork.CompanyRepository.GetCompanySubscribers(company.Id);
            if (subscriberUserIds.Any())
            {
                foreach (var userId in subscriberUserIds)
                {
                    var userNotification = new UserNotification
                    {
                        Title = $"New {vehicle.Brand} vehicle was posted!",
                        Body = $"<p>Company <strong>{company.Name}</strong> posted a new vehicle: {vehicle.Brand} {vehicle.Model} {vehicle.Year}.</p> <p><a href='http://localhost:5173/post/{post.Id}'>Click here to view the post</a></p>",
                        UserId = userId
                    };

                    _unitOfWork.NotificationRepository.Add<UserNotification>(userNotification);
                }

                await _unitOfWork.SaveAsync();
            }
        });

        return post;
    }
}