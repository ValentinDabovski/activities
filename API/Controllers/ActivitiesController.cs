using Application.Activities;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return HandleResult(
            await Mediator.Send(new List.Query()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return HandleResult(
            await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ActivityDto activity)
    {
        return HandleResult(
            await Mediator.Send(new Create.Command { Activity = activity }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, ActivityDto activity)
    {
        return HandleResult(
            await Mediator.Send(new Edit.Command { Activity = activity, ActivityId = id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleResult(
            await Mediator.Send(new Delete.Command { Id = id }));
    }
}