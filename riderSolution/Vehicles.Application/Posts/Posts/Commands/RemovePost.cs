using MediatR;
using Vehicles.Application.Abstractions;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.Application.Posts.Posts.Commands;

public record RemovePost(int Id) : IRequest;

public class RemovePostHandler : IRequestHandler<RemovePost>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemovePostHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(RemovePost request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        Post? post = await _unitOfWork.PostRepository.GetByIdAsync(request.Id);
        if (post == null) return;

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.RemoveAsync(request.Id);
                await _unitOfWork.SaveAsync();
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}