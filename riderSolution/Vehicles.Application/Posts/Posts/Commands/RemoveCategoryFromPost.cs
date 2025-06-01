using MediatR;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Posts.Posts.Commands;

public record RemoveCategoryFromPost(int PostId, int CategoryId) : IRequest;

public class RemoveCategoryFromPostHandler : IRequestHandler<RemoveCategoryFromPost>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCategoryFromPostHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCategoryFromPost request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.RemoveCategoryFromPostAsync(request.PostId, request.CategoryId);
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