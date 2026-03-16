using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.AccountCommands.CreateAccount;
using Application.Commands.AccountCommands.DeleteAccount;
using Application.Commands.AccountCommands.UpdateAccount;
using Swashbuckle.AspNetCore.Annotations;
using Domain.Entities;
using Application.Queries.BaseQueries.GetAllEntities;
using Application.Queries.BaseQueries.GetEntityById;

namespace Api.Controllers;

[Route("api/[controller]")]
public class AccountController : BaseController
{
  public AccountController(IMediator mediator) : base(mediator)
  {
  }

  [HttpPost]
  [SwaggerOperation(Summary = "Create account", Description = "Creates a new account for a user.")]
  [ProducesResponseType(typeof(AccountDto), (int)HttpStatusCode.Created)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update account", Description = "Updates an existing account details.")]
  [ProducesResponseType(typeof(AccountDto), (int)HttpStatusCode.OK)]
  [ProducesResponseType((int)HttpStatusCode.NotFound)]
  [ProducesResponseType((int)HttpStatusCode.BadRequest)]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountCommand command)
  {
    var result = await _mediator.Send(command with { AccountId = id });
    if (result == null) return NotFound();
    return Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete account")]
  [ProducesResponseType((int)HttpStatusCode.NoContent)]
  [ProducesResponseType((int)HttpStatusCode.NotFound)]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteAccountCommand(id));
    if (result == null) return NotFound();
    return NoContent();
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all accounts")]
  public async Task<ActionResult<List<Account>>> GetAll()
  {
    var results = await _mediator.Send(new GetAllEntitiesQuery<Account>());
    return Ok(results);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get account by ID")]
  public async Task<ActionResult<Account>> GetById(int id)
  {
    var result = await _mediator.Send(new GetEntityByIdQuery<Account>(id));
    if (result == null) return NotFound();
    return Ok(result);
  }
}
