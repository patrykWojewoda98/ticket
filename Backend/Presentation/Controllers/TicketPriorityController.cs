using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketPriorityCommands.CreateTicketPriority;
using Application.Commands.TicketPriorityCommands.DeleteTicketPriority;
using Application.Commands.TicketPriorityCommands.UpdateTicketPriority;
using Application.Queries.TicketPriorityQueries.GetAllTicketPriorities;
using Application.Queries.TicketPriorityQueries.GetTicketPriorityById;
using Application.Queries.TicketPriorityQueries.FindByName;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketPriorityController : BaseController
{
  public TicketPriorityController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create ticket priority")]
  [ProducesResponseType(typeof(TicketPriorityDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketPriorityCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketPriorityById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket priority")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketPriorityCommand command)
  {
    var result = await _mediator.Send(command with { TicketPriorityId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete ticket priority")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketPriorityCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all ticket priorities")]
  public async Task<ActionResult<List<TicketPriorityDto>>> GetAllTicketPriorities()
  {
    var result = await _mediator.Send(new GetAllTicketPrioritiesQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket priority by unique record ID")]
  public async Task<ActionResult<TicketPriorityDto>> GetTicketPriorityById(int id)
  {
    var result = await _mediator.Send(new GetTicketPriorityByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("name/{name}")]
  [SwaggerOperation(Summary = "Find ticket priority by its name")]
  public async Task<ActionResult<TicketPriorityDto>> FindByName(string name)
  {
    var result = await _mediator.Send(new FindByNameQuery(name));
    return (result == null) ? NotFound() : Ok(result);
  }
}
