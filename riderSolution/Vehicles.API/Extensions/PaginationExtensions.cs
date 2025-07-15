using AutoMapper;
using Vehicles.Application.PaginationModels;

namespace Vehicles.API.Extensions;

public static class PaginationExtensions
{
    public static PaginatedResult<TDestination> MapPaginatedResult<TSource, TDestination>(
        this PaginatedResult<TSource> source,
        IMapper mapper)
        where TSource : class
        where TDestination : class
    {
        return new PaginatedResult<TDestination>
        {
            Items = source.Items.Select(item => mapper.Map<TDestination>(item)).ToList(),
            PageIndex = source.PageIndex,
            PageSize = source.PageSize,
            Total = source.Total,
        };
    }
    
    public static async Task<PaginatedResult<TDestination>> MapPaginatedResultAsync<TSource, TDestination>(
        this PaginatedResult<TSource> source,
        Func<TSource, Task<TDestination?>> mapFunc)
        where TSource : class
        where TDestination : class
    {
        var dtoItems = new List<TDestination>();

        foreach (var item in source.Items)
        {
            var dto = await mapFunc(item);
            if (dto != null) dtoItems.Add(dto);
        }
        
        return new PaginatedResult<TDestination>
        {
            Items = dtoItems,
            PageIndex = source.PageIndex,
            PageSize = source.PageSize,
            Total = source.Total,
        };
    }
}