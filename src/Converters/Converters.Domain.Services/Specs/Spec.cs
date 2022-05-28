using System.Linq.Expressions;
using Converters.Domain.Services.Helpers;

namespace Converters.Domain.Services.Specs;

public class Spec<T> where T : class
{
    public Expression<Func<T, bool>> Expression { get; }
    public bool IsSatisfiedBy(T obj) => CompiledExpressions<T, bool>.AsFunc(Expression)(obj);
    
    public Spec(Expression<Func<T, bool>> expression)
    {
        Expression = expression;
    }

    public static bool operator false(Spec<T> _) => false;

    public static bool operator true(Spec<T> _) => false;

    public static Spec<T> operator &(Spec<T> first, Spec<T> second)
        => new(first.Expression.And(second.Expression));
    
    public static Spec<T> operator |(Spec<T> first, Spec<T> second)
        => new(first.Expression.Or(second.Expression));
    
    public static Spec<T> operator !(Spec<T> spec)
        => new(spec.Expression.Not());

    public static implicit operator Expression<Func<T, bool>>(Spec<T> spec)
        => spec.Expression;

    public static implicit operator Func<T, bool>(Spec<T> spec)
        => spec.IsSatisfiedBy;
}