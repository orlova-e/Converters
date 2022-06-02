using Converters.Domain.Interfaces;
using Converters.Infrastructure.DataAccess;
using FluentValidation;

namespace Converters.Web.API.Services.Validation.Helpers;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<TEditorDto, Guid> Exists<TEntity, TEditorDto>(
        this IRuleBuilder<TEditorDto, Guid> ruleBuilder, 
        IRepository repository)
        where TEntity : class, IDomainEntity
    {
        return ruleBuilder.SetValidator(new ExistsValidator<TEntity, TEditorDto>(repository));
    }
}