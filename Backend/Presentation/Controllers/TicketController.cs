using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketCommands.CreateTicket;
using Application.Commands.TicketCommands.DeleteTicket;
using Application.Commands.TicketCommands.UpdateTicket;
using Application.Queries.TicketQueries.GetAllTickets;
using Application.Queries.TicketQueries.GetTicketById;
using Application.Queries.TicketQueries.FindByUserId;
using Application.Queries.TicketQueries.FindByAssigneeId;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketController : BaseController
{
  public TicketController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create a new support ticket")]
  [ProducesResponseType(typeof(TicketDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket details")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketCommand command)
  {
    var result = await _mediator.Send(command with { TicketId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete a ticket")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all tickets in the system")]
  public async Task<ActionResult<List<TicketDto>>> GetAllTickets()
  {
    var result = await _mediator.Send(new GetAllTicketsQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket by unique record ID")]
  public async Task<ActionResult<TicketDto>> GetTicketById(int id)
  {
    var result = await _mediator.Send(new GetTicketByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("user/{userId}")]
  [SwaggerOperation(Summary = "Get all tickets created by a specific user")]
  public async Task<ActionResult<List<TicketDto>>> FindByUserId(int userId)
  {
    var result = await _mediator.Send(new FindByUserIdQuery(userId));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("assignee/{assigneeId}")]
  [SwaggerOperation(Summary = "Get all tickets assigned to a specific person")]
  public async Task<ActionResult<List<TicketDto>>> FindByAssigneeId(int assigneeId)
  {
    var result = await _mediator.Send(new FindByAssigneeIdQuery(assigneeId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
