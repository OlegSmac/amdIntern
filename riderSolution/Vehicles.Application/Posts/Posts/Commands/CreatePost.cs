using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Posts.Posts.Responses;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.VehicleTypes.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record CreatePost(string Title, string Body, DateTime Date, int CompanyId, int VehicleId) : IRequest<PostDto>;

public class CreatePostHandler : IRequestHandler<CreatePost, PostDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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

    public async Task<PostDto> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Company? company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.CompanyId);
        Vehicle? vehicle = await _unitOfWork.VehicleRepository.GetByIdAsync(request.VehicleId);
        
        if (company is null || vehicle is null) throw new NullReferenceException("Both Company and Vehicle are required.");

        Post post = await CreatePostAsync(request, company, vehicle);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.CreateAsync(post);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return PostDto.FromPost(post);
    }
}