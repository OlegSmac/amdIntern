using Vehicles.Application.PaginationModels;
using Vehicles.Domain;

namespace Vehicles.Application.Abstractions;

public interface IRepository
{
    Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : BaseEntity;

    void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;
    
    void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;

    Task<TEntity> DeleteAsync<TEntity>(int id) where TEntity : BaseEntity;

    Task<PaginatedResult<TEntity>> GetPagedDataAsync<TEntity>(PagedRequest pagedRequest) where TEntity : BaseEntity;
}