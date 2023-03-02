using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Common;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator mediator;
        protected IMediator Mediator
             => mediator ?? HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.Succeeded)
                return Ok(result.Data);
            if (!result.Succeeded && result.Errors.Any(x => x.ToLower().Contains("not found")))
                return NotFound();

            return BadRequest(result.Errors);
        }

        protected IActionResult HandleResult(Result result)
        {
            if (result.Succeeded)
                return Ok();
            if (!result.Succeeded && result.Errors.Any(x => x.ToLower().Contains("not found")))
                return NotFound();

            return BadRequest(result.Errors);
        }
    }
}