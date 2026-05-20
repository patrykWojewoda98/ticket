using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketNotificationCommands.CreateTicketNotification;
using Application.Commands.TicketNotificationCommands.DeleteTicketNotification;
using Application.Commands.TicketNotificationCommands.UpdateTicketNotification;
using Application.Queries.TicketNotificationQueries.GetAllTicketNotifications;
using Application.Queries.TicketNotificationQueries.GetTicketNotificationById;
using Swashbuckle.AspNetCore.Annotations;
using Application.Queries.TicketAttachmentQueries.FindByTicketId;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketNotificationController : BaseController
{
  public TicketNotificationController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create ticket notification")]
  [ProducesResponseType(typeof(TicketNotificationDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketNotificationCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketNotificationById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket notification")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketNotificationCommand command)
  {
    var result = await _mediator.Send(command with { TicketNotificationId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete ticket notification")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketNotificationCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all ticket notifications")]
  public async Task<ActionResult<List<TicketNotificationDto>>> GetAllTicketNotifications()
  {
    var result = await _mediator.Send(new GetAllTicketNotificationsQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket notification by unique record ID")]
  public async Task<ActionResult<TicketNotificationDto>> GetTicketNotificationById(int id)
  {
    var result = await _mediator.Send(new GetTicketNotificationByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("ticket/{ticketId}")]
  [SwaggerOperation(Summary = "Get all notifications for a specific ticket")]
  public async Task<ActionResult<List<TicketNotificationDto>>> FindByTicketId(int ticketId)
  {
    var result = await _mediator.Send(new FindByTicketIdQuery(ticketId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
