using Converters.Web.Models.Common;
using Converters.Web.Models.Convertations;
using Converters.Web.Services.Commands;
using Converters.Web.Services.Commands.Convertations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Converters.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> List([FromQuery] GetEntitiesDto dto)
    {
        var result = await _mediator.Send(new GetConvertationsRequest(dto));
        return this.AsViewResult(result);
    }
    
    [HttpGet]
    [Route("download")]
    public async Task<IActionResult> GetAsync([FromQuery] DownloadConvertationDto dto)
    {
        var result = await _mediator.Send(new GetConvertationRequest(dto));
        return this.Unwrap(result);
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Send(ConvertFileDto dto)
    {
        var result = await _mediator.Send(new ConvertFileRequest(dto));
        return this.Unwrap(result, "list", "home");
    }

    public IActionResult Error()
    {
        return View();
    }
}