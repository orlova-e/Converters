using Converters.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace Converters.Web.Services.Commands;

public static class HandlerResultExtensions
{
    public static IActionResult Unwrap<T>(
        this ControllerBase controller,
        HandlerResult<T> result,
        string action = "",
        string controllerRoute = "Index")
    {
        if (result is FileContent fileContext)
            return new FileStreamResult(fileContext.Content, fileContext.ContentType)
            {
                FileDownloadName = fileContext.FileName
            };
        
        return result.Status switch
        {
            OperationStatus.Success => controller.RedirectToAction(action, controllerRoute),
            OperationStatus.InternalError => controller.Problem(result.FailureMessage),
            { } => controller.Problem()
        };
    }
}