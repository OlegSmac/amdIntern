using MediatR;
using Microsoft.Extensions.Logging;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record UpdatePost(int Id, string Title, string Body, DateTime Date, int CompanyId, int VehicleId) : IRequest<Post>;

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
        post.Title = request.Title;
        post.Body = request.Body;
        post.Date = request.Date;
        post.Company = company;
        post.Vehicle = vehicle;
    }

    public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdatePost was called");
        ArgumentNullException.ThrowIfNull(request);
        
        try
        {
            Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.CompanyId);
            Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.VehicleId);
        
            if (company is null || vehicle is null) throw new NullReferenceException("Both Company and Vehicle are required.");
        
            Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);
            if (post == null) throw new NullReferenceException($"Post with id {request.Id} not found.");

            await UpdatePostAsync(post, request, company, vehicle);
            
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.UpdateAsync(post);
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