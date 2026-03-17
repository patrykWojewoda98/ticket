using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.SessionCommands.CreateSession;
using Application.Commands.SessionCommands.DeleteSession;
using Application.Commands.SessionCommands.UpdateSession;
using Application.Queries.SessionQueries.GetAllSessions;
using Application.Queries.SessionQueries.GetSessionById;
using Application.Queries.SessionQueries.FindByUserId;
using Application.Queries.SessionQueries.FindByToken;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class SessionController : BaseController
{
  public SessionController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create session")]
  [ProducesResponseType(typeof(SessionDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateSessionCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetSessionById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update session")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateSessionCommand command)
  {
    var result = await _mediator.Send(command with { SessionId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete session")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteSessionCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all sessions")]
  public async Task<ActionResult<List<SessionDto>>> GetAllSessions()
  {
    var result = await _mediator.Send(new GetAllSessionsQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get session by unique record ID")]
  public async Task<ActionResult<SessionDto>> GetSessionById(int id)
  {
    var result = await _mediator.Send(new GetSessionByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("user/{userId}")]
  [SwaggerOperation(Summary = "Get all sessions for a specific user")]
  public async Task<ActionResult<List<SessionDto>>> FindByUserId(int userId)
  {
    var result = await _mediator.Send(new FindByUserIdQuery(userId));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("token/{token}")]
  [SwaggerOperation(Summary = "Find session by token string")]
  public async Task<ActionResult<SessionDto>> FindByToken(string token)
  {
    var result = await _mediator.Send(new FindByTokenQuery(token));
    return (result == null) ? NotFound() : Ok(result);
  }
}
