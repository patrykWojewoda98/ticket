using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketStatusCommands.CreateTicketStatus;
using Application.Commands.TicketStatusCommands.DeleteTicketStatus;
using Application.Commands.TicketStatusCommands.UpdateTicketStatus;
using Application.Queries.TicketStatusQueries.GetAllTicketStatuses;
using Application.Queries.TicketStatusQueries.GetTicketStatusById;
using Application.Queries.TicketStatusQueries.FindByName;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketStatusController : BaseController
{
  public TicketStatusController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create ticket status")]
  [ProducesResponseType(typeof(TicketStatusDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketStatusCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketStatusById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket status")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketStatusCommand command)
  {
    var result = await _mediator.Send(command with { TicketStatusId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete ticket status")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketStatusCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all ticket statuses")]
  public async Task<ActionResult<List<TicketStatusDto>>> GetAllTicketStatuses()
  {
    var result = await _mediator.Send(new GetAllTicketStatusesQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket status by unique record ID")]
  public async Task<ActionResult<TicketStatusDto>> GetTicketStatusById(int id)
  {
    var result = await _mediator.Send(new GetTicketStatusByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("name/{name}")]
  [SwaggerOperation(Summary = "Find ticket status by its name")]
  public async Task<ActionResult<TicketStatusDto>> FindByName(string name)
  {
    var result = await _mediator.Send(new FindByNameQuery(name));
    return (result == null) ? NotFound() : Ok(result);
  }
}
