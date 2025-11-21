using System.Linq.Expressions;
using MedicalRecords.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastructure.Repositories;

public class EfRepository<T>(AppDbContext context) : IRepository<T> where T : class
{
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes)
    {
        var query = context.Set<T>().AsQueryable();
        
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Property(parameter, "Id");
        var constant = Expression.Constant(id);
        var equality = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
        
        var entity = await query
            .FirstOrDefaultAsync(lambda, cancellationToken);
        
        return entity;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await GetByIdAsync(id, cancellationToken, includes: null);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await context.Set<T>()
            .ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes)
    {
        var query = context.Set<T>().AsQueryable();
        
        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }
        
        return await query.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        context.Set<T>().Update(entity);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity != null)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}

