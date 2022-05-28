using Converters.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Converters.Domain.Entities;

namespace Converters.Infrastructure.DataAccess.Implementation;

internal static class RepositoryExtensions
{
    public static IQueryable<TEntity> MatchInclude<TEntity, TKey>(this IQueryable<TEntity> source)
        where TEntity : class, IEntity<TKey>, new()
    {
        var entity = new TEntity();

        return entity switch
        {
            Convertation _ => (IQueryable<TEntity>) (source as IQueryable<Convertation>)
                .Include(x => x.JsonFile)
                .Include(x => x.XmlFile),
            _ => source
        };
    }
}