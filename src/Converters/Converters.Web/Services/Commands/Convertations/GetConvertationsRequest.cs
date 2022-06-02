using Converters.Web.Models.Common;
using Converters.Web.Models.Convertations;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class GetConvertationsRequest : IRequest<HandlerResult<GetConvertationsDto>>
{
    public GetEntitiesDto Dto { get; }

    public GetConvertationsRequest(GetEntitiesDto dto)
    {
        Dto = dto;
    }
}