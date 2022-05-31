using Converters.Web.Models.Convertations;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class ConvertFileRequest : IRequest<HandlerResult<Unit>>
{
    public ConvertFileDto Dto { get; }

    public ConvertFileRequest(ConvertFileDto dto)
    {
        Dto = dto;
    }
}