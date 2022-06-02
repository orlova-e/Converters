using System.Linq.Expressions;
using Converters.Domain.Interfaces;
using Converters.Domain.Conditions;

namespace Converters.Infrastructure.DataAccess;

public interface IRepository
{
    T Get<T, TKey>(TKey id) where T : class, IEntity<TKey>, new();
    
    T Get<T, TKey>(Expression<Func<T, bool>> wherePredicate)
        where T : class, IEntity<TKey>, new();

    Task<T> GetAsync<T, TId>(TId id, CancellationToken cancellationToken)
        where T : class, IEntity<TId>, new();

    Task<T> GetAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new();

    Task<List<T>> ListAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new();

    Task<List<T>> ListAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, string orderByPredicate,
        SortDir sortDir, int? skip, int? take, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>, new();
    
    int Count<T, TKey>(Expression<Func<T, bool>> wherePredicate)
        where T : class, IEntity<TKey>;

    Task<int> CountAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task<T> CreateAsync<T, TKey>(T entity, CancellationToken cancellationToken) 
        where T : class, IEntity<TKey>;

    Task CreateAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task UpdateAsync<T, TKey>(T entity, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task UpdateAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task DeleteAsync<T, TKey>(TKey id, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task DeleteAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;
}