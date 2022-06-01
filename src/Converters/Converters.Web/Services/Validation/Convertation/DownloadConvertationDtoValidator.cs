using Converters.Domain.Conditions;
using Converters.Infrastructure.DataAccess;
using Converters.Web.Models.Convertations;
using FluentValidation;
using Converters.Web.API.Services.Validation.Helpers;

namespace Converters.Web.Services.Validation.Convertation;

public class DownloadConvertationDtoValidator : AbstractValidator<DownloadConvertationDto>
{
    public DownloadConvertationDtoValidator(IRepository repository)
    {
        RuleFor(x => x.Id)
            .Exists<Domain.Entities.Convertation, DownloadConvertationDto>(repository);

        RuleFor(x => x.Type)
            .Must(IsTypeValid);
    }

    private bool IsTypeValid(FileType type)
        => Enum.IsDefined(type);
}