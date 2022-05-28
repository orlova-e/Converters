using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Converters.Domain.Services.Specs;

internal class CompiledExpressions<TIn, TOut>
{
    private static readonly ConcurrentDictionary<Expression<Func<TIn, TOut>>, Func<TIn, TOut>> Cache = new();

    internal static Func<TIn, TOut> AsFunc(Expression<Func<TIn, TOut>> expression)
        => Cache.GetOrAdd(expression, k => k.Compile());
}