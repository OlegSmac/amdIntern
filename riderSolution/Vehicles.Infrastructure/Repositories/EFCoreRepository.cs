using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Vehicles.Application.Abstractions;
using Vehicles.Application.Extensions;
using Vehicles.Application.PaginationModels;
using Vehicles.Domain;

namespace Vehicles.Infrastructure.Repositories;

public class EFCoreRepository : IRepository
{
    protected readonly VehiclesDbContext _dbContext;
    private readonly IMapper _mapper;

    public EFCoreRepository(VehiclesDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : BaseEntity
    {
        return await _dbContext.FindAsync<TEntity>(id);
    }

    public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        _dbContext
            .Set<TEntity>()
            .Add(entity);
    }
    
    public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        _dbContext
            .Set<TEntity>()
            .Update(entity);
    }

    public async Task<TEntity> DeleteAsync<TEntity>(int id) where TEntity : BaseEntity
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
        {
            throw new ValidationException($"Object of type {typeof(TEntity)} with id { id } not found");
        }

        _dbContext.Set<TEntity>().Remove(entity);

        return entity;
    }

    public async Task<PaginatedResult<TEntity>> GetPagedDataAsync<TEntity>(PagedRequest pagedRequest) where TEntity : BaseEntity
    {
        return await _dbContext
            .Set<TEntity>()
            .CreatePaginatedResultAsync<TEntity>(pagedRequest, _mapper);
    }
}