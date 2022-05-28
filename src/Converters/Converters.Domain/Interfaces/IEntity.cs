namespace Converters.Domain.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
        bool IsDeleted { get; }
    }
}
