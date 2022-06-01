using FluentValidation;
using FluentValidation.Validators;
using Converters.Domain.Interfaces;
using Converters.Domain.Services.Specs.Domain;
using Converters.Infrastructure.DataAccess;

namespace Notes.Web.API.Services.Validation.Helpers;

public class ExistsValidator<TEntity, TDto> : AsyncPropertyValidator<TDto, Guid>
    where TEntity : class, IDomainEntity
{
    private readonly IRepository _repository;
    
    public override string Name => nameof(ExistsValidator<TEntity, TDto>);

    public ExistsValidator(IRepository repository)
        => _repository = repository;

    public override async Task<bool> IsValidAsync(
        ValidationContext<TDto> context, 
        Guid value, 
        CancellationToken cancellation)
    {
        var count = await _repository.CountAsync<TEntity, Guid>(
            Common.NotDeleted<TEntity>(value), 
            cancellation);
        
        return count is 1;
    }
    
    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Entity of type '{typeof(TEntity).Name}' does not exist";
}