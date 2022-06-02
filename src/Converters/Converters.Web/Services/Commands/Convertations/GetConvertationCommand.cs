using Converters.Domain.Entities;
using Converters.Domain.Services.Specs.Domain;
using Converters.Infrastructure.DataAccess;
using Converters.Services.Interfaces;
using Converters.Web.Models.Common;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class GetConvertationCommand : IRequestHandler<GetConvertationRequest, HandlerResult<FileContent>>
{
    private readonly ILogger<GetConvertationCommand> _logger;
    private readonly IRepository _repository;
    private readonly IFileManager _fileManager;

    public GetConvertationCommand(
        ILogger<GetConvertationCommand> logger,
        IRepository repository,
        IFileManager fileManager)
    {
        _logger = logger;
        _repository = repository;
        _fileManager = fileManager;
    }
    
    public async Task<HandlerResult<FileContent>> Handle(GetConvertationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var convertation = await _repository.GetAsync<Convertation, Guid>(
                Common.NotDeleted<Convertation>(request.Dto.Id), cancellationToken);

            var fileName = Path.ChangeExtension(convertation.Name, Enum.GetName(request.Dto.Type).ToLower());
            var file = _fileManager.Get(fileName);

            var fileContent = new FileContent(fileName, FileContent.DefaultContentType, file);
            return HandlerResult<FileContent>.Success(fileContent);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't get a file:\n{message}\n{stackTrace}",
                exc.Message, exc.StackTrace);
                        
            return HandlerResult<FileContent>.Exception();
        }
    }
}