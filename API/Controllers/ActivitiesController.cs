using Application.Activities;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ActivityDto>>> Get()
        {
            var result = await Mediator.Send(new List.Query());

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityDto activityDto)
        {
            var result = await Mediator.Send(new Create.Command { ActivityDto = activityDto });

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, ActivityDto activityDto)
        {
            var result = await Mediator.Send(new Edit.Command { ActivityDto = activityDto, AcivityId = id });

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new Delete.Command { Id = id });

            if (!result.Succeeded) return BadRequest(result.Errors);
            
            return Ok();
        }
    }
}