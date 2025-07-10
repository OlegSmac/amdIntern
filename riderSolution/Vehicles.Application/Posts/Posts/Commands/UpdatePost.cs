using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record UpdatePost(Post Post) : IRequest<Post>;

public class UpdatePostHandler : IRequestHandler<UpdatePost, Post>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePostHandler> _logger;

    public UpdatePostHandler(IUnitOfWork unitOfWork, ILogger<UpdatePostHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    private async Task UpdatePostAsync(Post post, UpdatePost request, Company company, Vehicle vehicle)
    {
        post.Title = request.Post.Title;
        post.Body = request.Post.Body;
        post.Date = request.Post.Date;
        post.Views = request.Post.Views;
        post.IsHidden = request.Post.IsHidden;
        post.Price = request.Post.Price;
        post.Company = company;
        post.Vehicle = vehicle;

        _unitOfWork.ImageRepository.RemoveImages(post.Id);
        await _unitOfWork.SaveAsync();
        
        post.Images.Clear();
        post.Images = request.Post.Images
            .Select(image => new PostImage { Url = image.Url, PostId = post.Id })
            .ToList();
        
        var currentCategories = post.Categories.ToList();
        
        var updatedCategoryNames = request.Post.Categories.Select(c => c.Name).ToList();
        var updatedCategories = await Task.WhenAll(
            updatedCategoryNames.Select(uc => _unitOfWork.CategoryRepository.GetByNameAsync(uc))
        );
        
        foreach (var category in currentCategories)
        {
            if (!updatedCategoryNames.Contains(category.Name))
            {
                post.Categories.Remove(category);
            }
        }
        
        foreach (var category in updatedCategories)
        {
            if (category != null && post.Categories.All(c => c.Name != category.Name))
            {
                post.Categories.Add(category);
            }
        }

    }

    public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdatePost was called");
        ArgumentNullException.ThrowIfNull(request);
        
        var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Post.CompanyId);
        var vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync<Vehicle>(request.Post.VehicleId);

        Console.WriteLine($"{company} {vehicle}");
        if (company == null || vehicle == null) throw new NullReferenceException("Both Company and Vehicle are required.");

        var post = await _unitOfWork.PostRepository.GetByIdAsync<Post>(request.Post.Id);
        if (post == null) throw new NullReferenceException($"Post with id {request.Post.Id} not found.");

        await UpdatePostAsync(post, request, company, vehicle);

        await _unitOfWork.ExecuteTransactionAsync(async () =>
        {
            _unitOfWork.PostRepository.Update<Post>(post);
            await _unitOfWork.SaveAsync();
        });

        return post;
    }
}
