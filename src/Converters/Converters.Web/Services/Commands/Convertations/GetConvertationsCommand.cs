using Converters.Domain.Conditions;
using Converters.Domain.Entities;
using Converters.Domain.Services.Specs.Domain;
using Converters.Infrastructure.DataAccess;
using Converters.Web.Models.Convertations;
using Converters.Web.Services.Interfaces;
using MediatR;

namespace Converters.Web.Services.Commands.Convertations;

public class GetConvertationsCommand : IRequestHandler<GetConvertationsRequest, HandlerResult<GetConvertationsDto>>
{
    private readonly ILogger<GetConvertationCommand> _logger;
    private readonly IRepository _repository;
    private readonly ITranslator _translator;

    public GetConvertationsCommand(
        ILogger<GetConvertationCommand> logger,
        IRepository repository,
        ITranslator translator)
    {
        _logger = logger;
        _repository = repository;
        _translator = translator;
    }
    
    public async Task<HandlerResult<GetConvertationsDto>> Handle(GetConvertationsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _repository.ListAsync<Convertation, Guid>(
                wherePredicate: Common.NotDeleted<Convertation>(),
                orderByPredicate: nameof(Convertation.Created),
                sortDir: SortDir.Desc,
                skip: (request.Dto.ItemsNumber * (request.Dto.Page - 1)),
                take: request.Dto.ItemsNumber,
                cancellationToken);
        
            var totalEntitiesCount = await _repository.CountAsync<Convertation, Guid>(
                Common.NotDeleted<Convertation>(), cancellationToken);
        
            var viewModels = _translator.Translate<IEnumerable<Convertation>, IEnumerable<GetConvertationDto>>(entities);

            var listDto = new GetConvertationsDto
            {
                CurrentPage = request.Dto.Page,
                ItemsPerPage = request.Dto.ItemsNumber,
                TotalPages = (int) Math.Ceiling((double) totalEntitiesCount / request.Dto.ItemsNumber),
                TotalItems = totalEntitiesCount,
                Entities = viewModels
            };
        
            _logger.LogInformation("{count} of convertations were recieved", entities.Count);
            return HandlerResult<GetConvertationsDto>.Success(listDto);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't get convertations:\n{message}\n{stackTrace}",
                exc.Message, exc.StackTrace);
            
            return HandlerResult<GetConvertationsDto>.Exception();
        }
    }
}