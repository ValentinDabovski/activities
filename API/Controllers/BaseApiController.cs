using Application.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator
        => _mediator ?? HttpContext.RequestServices.GetService<IMediator>();

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        return result.Succeeded switch
        {
            true => Ok(result.Data),
            false when result.Errors.Any(x => x.ToLower().Contains("not found")) => NotFound(),
            _ => BadRequest(result.Errors)
        };
    }

    protected IActionResult HandleResult(Result result)
    {
        return result.Succeeded switch
        {
            true => Ok(),
            false when result.Errors.Any(x => x.ToLower().Contains("not found")) => NotFound(),
            _ => BadRequest(result.Errors)
        };
    }
}