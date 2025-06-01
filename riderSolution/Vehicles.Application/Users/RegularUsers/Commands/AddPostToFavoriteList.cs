using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;
using Vehicles.Domain.Users.Models;
using Vehicles.Domain.Users.Relations;

namespace Vehicles.Application.Users.RegularUsers.Commands;

public record AddPostToFavoriteList(int UserId, int PostId) : IRequest<bool>;

public class AddPostToFavoriteListHandler : IRequestHandler<AddPostToFavoriteList, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddPostToFavoriteListHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    private async Task<FavoritePost> CreateFavoritePost(AddPostToFavoriteList request)
    {
        return new FavoritePost()
        {
            UserId = request.UserId,
            PostId = request.PostId,
            Date = DateTime.Now
        };
    }

    public async Task<bool> Handle(AddPostToFavoriteList request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RegularUser? user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null) throw new KeyNotFoundException($"User with ID {request.UserId} not found");
        
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.PostId);
        if (post == null) throw new KeyNotFoundException($"Post with ID {request.PostId} not found");
        
        FavoritePost favoritePost = await CreateFavoritePost(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.UserRepository.AddPostToFavoriteListAsync(favoritePost);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return true;
    }
}