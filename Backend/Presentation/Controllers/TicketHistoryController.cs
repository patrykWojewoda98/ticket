using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketHistoryCommands.CreateTicketHistory;
using Application.Commands.TicketHistoryCommands.DeleteTicketHistory;
using Application.Commands.TicketHistoryCommands.UpdateTicketHistory;
using Application.Queries.TicketHistoryQueries.GetAllTicketHistories;
using Application.Queries.TicketHistoryQueries.GetTicketHistoryById;
using Application.Queries.TicketHistoryQueries.FindByTicketId;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketHistoryController : BaseController
{
  public TicketHistoryController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create ticket history entry")]
  [ProducesResponseType(typeof(TicketHistoryDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketHistoryCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketHistoryById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket history entry")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketHistoryCommand command)
  {
    var result = await _mediator.Send(command with { TicketHistoryId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete ticket history entry")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketHistoryCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all ticket history entries")]
  public async Task<ActionResult<List<TicketHistoryDto>>> GetAllTicketHistories()
  {
    var result = await _mediator.Send(new GetAllTicketHistoriesQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket history entry by unique record ID")]
  public async Task<ActionResult<TicketHistoryDto>> GetTicketHistoryById(int id)
  {
    var result = await _mediator.Send(new GetTicketHistoryByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("ticket/{ticketId}")]
  [SwaggerOperation(Summary = "Get full history log for a specific ticket")]
  public async Task<ActionResult<List<TicketHistoryDto>>> FindByTicketId(int ticketId)
  {
    var result = await _mediator.Send(new FindByTicketIdQuery(ticketId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
