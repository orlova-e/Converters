using Converters.Domain.Conditions;
using Converters.Domain.Entities;
using Converters.Domain.Services.Specs.Domain;
using Converters.Infrastructure.DataAccess;
using Converters.Web.Models.Common;
using Converters.Web.Models.Convertations;
using Converters.Web.Services.Interfaces;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class GetConvertationsCommand : IRequestHandler<GetConvertationsRequest, HandlerResult<ListDto<GetConvertationDto>>>
{
    private readonly ILogger<GetConvertationCommand> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository _repository;
    private readonly ITranslator _translator;

    public GetConvertationsCommand(
        ILogger<GetConvertationCommand> logger,
        IHttpContextAccessor httpContextAccessor,
        IRepository repository,
        ITranslator translator)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _repository = repository;
        _translator = translator;
    }
    
    public async Task<HandlerResult<ListDto<GetConvertationDto>>> Handle(GetConvertationsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var sessionId = _httpContextAccessor.HttpContext.Session.Id;

            var entities = await _repository.ListAsync<Convertation, Guid>(
                wherePredicate: Common.NotDeleted<Convertation>() &
                    Domain.Services.Specs.Domain.Convertations.BySessionId(sessionId),
                orderByPredicate: nameof(Convertation.Created),
                sortDir: SortDir.Desc,
                skip: request.Dto.Page * request.Dto.ItemsNumber,
                take: request.Dto.ItemsNumber,
                cancellationToken);
        
            var totalEntitiesCount = await _repository.CountAsync<Convertation, Guid>(
                Common.NotDeleted<Convertation>(), cancellationToken);
        
            var viewModels = _translator.Translate<IEnumerable<Convertation>, IEnumerable<GetConvertationDto>>(entities);

            var listDto = new ListDto<GetConvertationDto>
            {
                CurrentPage = request.Dto.Page,
                ItemsPerPage = request.Dto.ItemsNumber,
                TotalPages = (int) Math.Ceiling((double) totalEntitiesCount / request.Dto.ItemsNumber),
                TotalItems = totalEntitiesCount,
                Entities = viewModels
            };
        
            _logger.LogInformation("{count} of convertations were recieved", entities.Count);
            return HandlerResult<ListDto<GetConvertationDto>>.Success(listDto);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't get convertations:\n{message}\n{stackTrace}",
                exc.Message, exc.StackTrace);
            
            return HandlerResult<ListDto<GetConvertationDto>>.Exception();
        }
    }
}