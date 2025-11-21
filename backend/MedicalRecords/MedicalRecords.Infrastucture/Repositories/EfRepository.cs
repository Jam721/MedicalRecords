using MedicalRecords.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedicalRecords.Infrastucture.Repositories;

public class EfRepository<T>(AppDbContext context) : IRepository<T> where T : class
{
    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Set<T>()
            .FindAsync(id, cancellationToken);
        
        return entity;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await context.Set<T>()
            .ToListAsync(cancellationToken);
        
        return entities;
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}