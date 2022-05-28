using System.Linq.Expressions;
using Converters.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Converters.Domain.Conditions;
using Converters.Infrastructure.Base.Configuration;

namespace Converters.Infrastructure.DataAccess.Implementation;

internal class Repository : IRepository
{
    private readonly Context _context;

    public Repository(Context context)
    {
        _context = context;
    }

    public T Get<T, TKey>(TKey id) where T : class, IEntity<TKey>, new()
    {
        return _context.Set<T>().Find(id);
    }

    public T Get<T, TKey>(Expression<Func<T, bool>> wherePredicate) where T : class, IEntity<TKey>, new()
    {
        return _context
            .Set<T>()
            .MatchInclude<T, TKey>()
            .FirstOrDefault(wherePredicate);
    }

    public Task<T> GetAsync<T, TKey>(TKey id, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new()
    {
        return _context
            .Set<T>()
            .FindAsync(new object[] { id }, cancellationToken: cancellationToken)
            .AsTask();
    }

    public Task<T> GetAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new()
    {
        return _context
            .Set<T>()
            .MatchInclude<T, TKey>()
            .FirstOrDefaultAsync(wherePredicate, cancellationToken);
    }

    public Task<List<T>> ListAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new()
    {
        return _context
            .Set<T>()
            .MatchInclude<T, TKey>()
            .ToListAsync(cancellationToken);
    }

    public Task<List<T>> ListAsync<T, TKey>(
        Expression<Func<T, bool>> wherePredicate,
        Expression orderByPredicate,
        SortDir sortDir,
        int? skip,
        int? take,
        CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new()
    {
        return _context
            .Set<T>()
            .Where(wherePredicate)
            .MatchInclude<T, TKey>()
            .Provider.CreateQuery<T>(orderByPredicate)
            .Skip(skip ?? 0)
            .Take(take ?? int.MaxValue)
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        if (wherePredicate is null)
        {
            return _context.Set<T>().CountAsync(cancellationToken);
        }
        
        return _context
            .Set<T>()
            .Where(wherePredicate)
            .CountAsync(cancellationToken);
    }

    public async Task<T> CreateAsync<T, TKey>(T entity, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task CreateAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        await _context.AddRangeAsync(entities, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync<T, TKey>(T entity, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        _context.Update(entity);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        _context.UpdateRange(entities);
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync<T, TKey>(TKey id, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        if (entity is null)
            return;
        
        _context.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>
    {
        _context.RemoveRange(entities);
        return _context.SaveChangesAsync(cancellationToken);
    }
}