using Converters.Domain.Entities;
using Converters.Infrastructure.DataAccess;
using Converters.Services.Interfaces;
using Converters.Web.Models.Convertations;
using Converters.Web.Services.Interfaces;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class ConvertFileCommand : IRequestHandler<ConvertFileRequest, HandlerResult<Unit>>
{
    private readonly ILogger<ConvertFileCommand> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFileManager _fileManager;
    private readonly IRepository _repository;
    private readonly ITranslator _translator;
    private readonly IMediator _mediator;

    public ConvertFileCommand(
        ILogger<ConvertFileCommand> logger,
        IDateTimeService dateTimeService,
        IHttpContextAccessor httpContextAccessor,
        IFileManager fileManager,
        IRepository repository,
        ITranslator translator,
        IMediator mediator)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _httpContextAccessor = httpContextAccessor;
        _fileManager = fileManager;
        _repository = repository;
        _translator = translator;
        _mediator = mediator;
    }
    
    public async Task<HandlerResult<Unit>> Handle(ConvertFileRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await using var memoryStream = new MemoryStream();
            await request.Dto.File.CopyToAsync(memoryStream, cancellationToken);
            
            var fileName = await _fileManager.SaveAsync(request.Dto.File.FileName, memoryStream);
            await _fileManager.ConvertAsync(fileName);
            
            var convertation = new Convertation
            {
                Name = Path.GetFileNameWithoutExtension(fileName),
                SessionId = _httpContextAccessor.HttpContext.Session.Id
            };
        
            _dateTimeService.Created(convertation);

           await _repository.CreateAsync<Convertation, Guid>(convertation, cancellationToken);
           
           var dto = _translator.Translate<Convertation, GetConvertationDto>(convertation);
           await _mediator.Publish(new FileConvertedEvent(dto), cancellationToken);
           
           return HandlerResult<Unit>.Success();
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't convert the file '{fileName}':\n{message}\n{stackTrace}",
                request.Dto.File.FileName, exc.Message, exc.StackTrace);
            
            return HandlerResult<Unit>.Exception();
        }
    }
}