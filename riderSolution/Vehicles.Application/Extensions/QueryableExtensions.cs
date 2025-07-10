using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Text;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain;

namespace Vehicles.Application.Extensions;

public static class QueryableExtensions
{
    public static async Task<PaginatedResult<TEntity>> CreatePaginatedResultAsync<TEntity>(this IQueryable<TEntity> query, PagedRequest pagedRequest, IMapper mapper)
        where TEntity : BaseEntity
    {
        if (pagedRequest == null) throw new ArgumentNullException(nameof(pagedRequest));
        
        query = query.ApplyFilters(pagedRequest);

        var total = await query.CountAsync();
        var result = query.Sort(pagedRequest).Paginate(pagedRequest);

        var listResult = await result.ToListAsync();

        return new PaginatedResult<TEntity>()
        {
            Items = listResult,
            PageSize = pagedRequest.PageSize,
            PageIndex = pagedRequest.PageIndex,
            Total = total
        };
    }

    private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedRequest pagedRequest)
    {
        var entities = query.Skip(pagedRequest.PageIndex * pagedRequest.PageSize).Take(pagedRequest.PageSize);
        return entities;
    }

    private static IQueryable<T> Sort<T>(this IQueryable<T> query, PagedRequest pagedRequest)
    {
        var column = pagedRequest.ColumnNameForSorting?.Trim();
        if (!string.IsNullOrWhiteSpace(column))
        {
            var direction = pagedRequest.SortDirection?.ToLower() == "desc" ? "descending" : "ascending";
            query = query.OrderBy($"{pagedRequest.ColumnNameForSorting} {direction}");
        }
        return query;
    }

    private static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, PagedRequest pagedRequest)
    {
        if (!pagedRequest.RequestFilters.Filters.Any()) return query;
        
        var predicate = new StringBuilder();
        var propertyValues = new List<object>();
        var requestFilters = pagedRequest.RequestFilters;
        var logicalOperator = requestFilters.LogicalOperator == FilterLogicalOperators.Or ? "or" : "and";
        
        for (int i = 0; i < requestFilters.Filters.Count; i++)
        {
            var filter = requestFilters.Filters[i];
            
            if (i > 0) predicate.Append($" {logicalOperator} ");

            if (filter.Path == "FavoritePosts.UserId")
            {
                predicate.Append($"FavoritePosts.Any(UserId == @{i})");
                propertyValues.Add(filter.Value);
            }
            else if (filter.Path == "Categories")
            {
                predicate.Append($"Categories.Any(Name == @{i})");
                propertyValues.Add(filter.Value);
            }
            else if (bool.TryParse(filter.Value, out bool boolValue))
            {
                predicate.Append($"{filter.Path} == @{i}");
                propertyValues.Add(boolValue);
            }
            else if (int.TryParse(filter.Value, out int intValue))
            {
                predicate.Append($"{filter.Path} == @{i}");
                propertyValues.Add(intValue);
            }
            else
            {
                predicate.Append($"{filter.Path}.Contains(@{i})");
                propertyValues.Add(filter.Value);
            }
        }
        
        query = query.Where(predicate.ToString(), propertyValues.ToArray());

        return query;
    }
}