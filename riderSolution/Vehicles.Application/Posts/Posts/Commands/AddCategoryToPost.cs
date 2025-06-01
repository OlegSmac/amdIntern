using MediatR;
using Vehicles.Application.Abstractions;

namespace Vehicles.Application.Posts.Posts.Commands;

public record AddCategoryToPost(int PostId, int CategoryId) : IRequest;

public class AddCategoryToPostHandler : IRequestHandler<AddCategoryToPost>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCategoryToPostHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddCategoryToPost request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _unitOfWork.PostRepository.AddCategoryToPostAsync(request.PostId, request.CategoryId);
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