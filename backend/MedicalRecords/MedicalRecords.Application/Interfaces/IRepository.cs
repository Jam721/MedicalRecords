using System.Linq.Expressions;

namespace MedicalRecords.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken, params Expression<Func<T, object>>[]? includes);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}