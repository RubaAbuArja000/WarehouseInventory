using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Warehouse.Core.Common;
using Warehouse.Core.Exceptions;
using Warehouse.Core.IRepositories;
using Warehouse.DataAccess.Persistence;

namespace Warehouse.DataAccess.Repositories;

public class BaseRepository<TEntity>(DatabaseContext Context, DbSet<TEntity> DbSet) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = (await DbSet.AddAsync(entity)).Entity;
        await Context.SaveChangesAsync();

        return addedEntity;
    }
    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var removedEntity = DbSet.Remove(entity).Entity;
        await Context.SaveChangesAsync();

        return removedEntity;
    }
    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }
    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await DbSet.Where(predicate).FirstOrDefaultAsync();

        if (entity == null) throw new ResourceNotFoundException(typeof(TEntity));

        return await DbSet.Where(predicate).FirstOrDefaultAsync();
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();

        return entity;
    }
}