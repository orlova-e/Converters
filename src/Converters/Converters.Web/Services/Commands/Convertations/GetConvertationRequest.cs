using Converters.Web.Models.Common;
using Converters.Web.Models.Convertations;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class GetConvertationRequest : IRequest<HandlerResult<FileContent>>
{
    public DownloadConvertationDto Dto { get; }

    public GetConvertationRequest(DownloadConvertationDto dto)
    {
        Dto = dto;
    }
}