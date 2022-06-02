using FluentValidation;
using FluentValidation.Validators;
using Converters.Domain.Interfaces;
using Converters.Domain.Services.Specs.Domain;
using Converters.Infrastructure.DataAccess;

namespace Converters.Web.API.Services.Validation.Helpers;

public class ExistsValidator<TEntity, TDto> : PropertyValidator<TDto, Guid>
    where TEntity : class, IDomainEntity
{
    private readonly IRepository _repository;

    public override bool IsValid(ValidationContext<TDto> context, Guid value)
    {
        var count = _repository.Count<TEntity, Guid>(
            Common.NotDeleted<TEntity>(value));
        
        return count is 1;
    }

    public override string Name => nameof(ExistsValidator<TEntity, TDto>);

    public ExistsValidator(IRepository repository)
        => _repository = repository;
    
    protected override string GetDefaultMessageTemplate(string errorCode)
        => $"Entity of type '{typeof(TEntity).Name}' does not exist";
}