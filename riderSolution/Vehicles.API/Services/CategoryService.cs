using MediatR;
using Vehicles.Application.Requests.Posts.Categories.Queries;
using Vehicles.Domain.Posts.Models;

namespace Vehicles.API.Services;

public class CategoryService
{
    public static async Task<List<Category>> CreateCategoryList(List<string> categories, IMediator mediator)
    {
        var res = new List<Category>();
        foreach (var categoryName in categories)
        {
            var category = await mediator.Send(new GetCategoryByName(categoryName));
            if (category != null) res.Add(category);
        }

        return res;
    }
}