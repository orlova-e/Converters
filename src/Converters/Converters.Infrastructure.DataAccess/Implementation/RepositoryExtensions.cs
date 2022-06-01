using System.Linq.Expressions;
using Converters.Domain.Conditions;

namespace Converters.Infrastructure.DataAccess.Implementation;

internal static class RepositoryExtensions
{
    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> source,
        string orderByProperty,
        SortDir sortDir)
    {
        var type = typeof(T);
        var property = type.GetProperty(orderByProperty);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var orderMethodName = sortDir == SortDir.Asc ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
        
        MethodCallExpression resultExpression = Expression.Call(
            typeof(Queryable),
            orderMethodName,
            new Type[] { type, property.PropertyType },
            source.Expression,
            Expression.Quote(orderByExpression));
        
        return source.Provider.CreateQuery<T>(resultExpression);
    }
}