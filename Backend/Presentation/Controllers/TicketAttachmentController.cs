using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.TicketAttachmentCommands.CreateTicketAttachment;
using Application.Commands.TicketAttachmentCommands.DeleteTicketAttachment;
using Application.Commands.TicketAttachmentCommands.UpdateTicketAttachment;
using Application.Queries.TicketAttachmentQueries.GetAllTicketAttachments;
using Application.Queries.TicketAttachmentQueries.GetTicketAttachmentById;
using Application.Queries.TicketAttachmentQueries.FindByTicketId;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class TicketAttachmentController : BaseController
{
  public TicketAttachmentController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Upload/Create ticket attachment")]
  [ProducesResponseType(typeof(TicketAttachmentDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateTicketAttachmentCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetTicketAttachmentById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update ticket attachment metadata")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketAttachmentCommand command)
  {
    var result = await _mediator.Send(command with { TicketAttachmentId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete ticket attachment")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteTicketAttachmentCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all ticket attachments")]
  public async Task<ActionResult<List<TicketAttachmentDto>>> GetAllTicketAttachments()
  {
    var result = await _mediator.Send(new GetAllTicketAttachmentsQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get ticket attachment by unique record ID")]
  public async Task<ActionResult<TicketAttachmentDto>> GetTicketAttachmentById(int id)
  {
    var result = await _mediator.Send(new GetTicketAttachmentByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("ticket/{ticketId}")]
  [SwaggerOperation(Summary = "Get all attachments for a specific ticket")]
  public async Task<ActionResult<List<TicketAttachmentDto>>> FindByTicketId(int ticketId)
  {
    var result = await _mediator.Send(new FindByTicketIdQuery(ticketId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
