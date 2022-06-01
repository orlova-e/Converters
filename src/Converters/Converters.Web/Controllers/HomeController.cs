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
    public async Task<IActionResult> Index([FromQuery] GetEntitiesDto dto)
    {
        var result = await _mediator.Send(new GetConvertationsRequest(dto));
        return this.Unwrap(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromRoute] DownloadConvertationDto dto)
    {
        var result = await _mediator.Send(new GetConvertationRequest(dto));
        return this.Unwrap(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> GetAsync([FromForm] ConvertFileDto dto)
    {
        var result = await _mediator.Send(new ConvertFileRequest(dto));
        return this.Unwrap(result);
    }

    public IActionResult Error()
    {
        return View();
    }
}