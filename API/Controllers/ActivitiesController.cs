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
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetById(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityDto activityDto)
        {
            return Ok(await Mediator.Send(new Create.Command { ActivityDto = activityDto }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, ActivityDto activityDto)
        {
            return Ok(await Mediator.Send(new Edit.Command { ActivityDto = activityDto, AcivityId = id }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return this.Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}