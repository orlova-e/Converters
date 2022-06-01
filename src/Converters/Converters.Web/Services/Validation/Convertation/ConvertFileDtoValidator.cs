using Converters.Web.Configuration;
using Converters.Web.Models.Convertations;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Converters.Web.Services.Validation.Convertation;

public class ConvertFileDtoValidator : AbstractValidator<ConvertFileDto>
{
    private readonly IOptions<ValidationOptions> _options;

    public ConvertFileDtoValidator(IOptions<ValidationOptions> options)
    {
        _options = options;
        
        RuleFor(x => x.File)
            .Must(IsExtensionValid);
    }

    private bool IsExtensionValid(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        return _options.Value.AllowedTypes.Contains(extension);
    }
}