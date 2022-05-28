using System.Linq.Expressions;

namespace Converters.Domain.Services.Helpers;

/// <summary>
/// <see href="https://petemontgomery.wordpress.com/2011/02/10/a-universal-predicatebuilder/">Pete Montgomery's universal PredicateBuilder</see>
/// </summary>
public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> True<T>()
    {
        return param => true;
    }

    public static Expression<Func<T, bool>> False<T>()
    {
        return param => false;
    }

    internal static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate)
    {
        return predicate;
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Compose(second, Expression.AndAlso);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
        return first.Compose(second, Expression.OrElse);
    }

    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
    {
        var negated = Expression.Not(expression.Body);
        return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
    }

    private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
    {
        var map = first.Parameters
            .Select((f, i) => new { f, s = second.Parameters[i] })
            .ToDictionary(p => p.s, p => p.f);
 
        var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
 
        return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
    }

    private class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;
 
        ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
 
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
 
        protected override Expression VisitParameter(ParameterExpression p)
        {
            ParameterExpression replacement;
 
            if (_map.TryGetValue(p, out replacement))
            {
                p = replacement;
            }
 
            return base.VisitParameter(p);
        }
    }
}