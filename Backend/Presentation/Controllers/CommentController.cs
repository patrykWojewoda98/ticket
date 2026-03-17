using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.CommentCommands.CreateComment;
using Application.Commands.CommentCommands.DeleteComment;
using Application.Commands.CommentCommands.UpdateComment;
using Application.Queries.CommentQueries.GetAllComments;
using Application.Queries.CommentQueries.GetCommentById;
using Application.Queries.CommentQueries.FindByTicketId;
using Application.Queries.CommentQueries.GetCommentsByUser;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class CommentController : BaseController
{
  public CommentController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create comment")]
  [ProducesResponseType(typeof(CommentDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetCommentById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update comment")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateCommentCommand command)
  {
    var result = await _mediator.Send(command with { CommentId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete comment")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteCommentCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all comments")]
  public async Task<ActionResult<List<CommentDto>>> GetAllComments()
  {
    var result = await _mediator.Send(new GetAllCommentsQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get comment by unique record ID")]
  public async Task<ActionResult<CommentDto>> GetCommentById(int id)
  {
    var result = await _mediator.Send(new GetCommentByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("ticket/{ticketId}")]
  [SwaggerOperation(Summary = "Get all comments for a specific ticket")]
  public async Task<ActionResult<List<CommentDto>>> GetCommentsByTicketId(int ticketId)
  {
    var result = await _mediator.Send(new GetCommentsByTicketQuery(ticketId));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("user/{userId}")]
  [SwaggerOperation(Summary = "Get all comments written by a specific user")]
  public async Task<ActionResult<List<CommentDto>>> GetCommentsByUser(int userId)
  {
    var result = await _mediator.Send(new GetCommentsByUserQuery(userId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
