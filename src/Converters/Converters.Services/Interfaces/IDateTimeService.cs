using Converters.Domain.Interfaces;

namespace Converters.Services.Interfaces;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
    void Created<T>(T entity) where T : class, IHistorical;
    void Updated<T>(T entity) where T : class, IHistorical;
    void Deleted<T>(T entity) where T : class, IHistorical;
}