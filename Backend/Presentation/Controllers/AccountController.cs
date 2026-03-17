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
using Application.Queries.AccountQueries.FindAccountsByUserId;
using Application.Queries.AccountQueries.FindAccountByProviderId;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class AccountController : BaseController
{
  public AccountController(IMediator mediator) : base(mediator) { }

  [HttpPost]
  [SwaggerOperation(Summary = "Create account")]
  [ProducesResponseType(typeof(AccountDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateAccountCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetAccountById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update account")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountCommand command)
  {
    var result = await _mediator.Send(command with { AccountId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete account")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteAccountCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all accounts")]
  public async Task<ActionResult<List<AccountDto>>> GetAllAccounts()
  {
    var result = await _mediator.Send(new GetAllEntitiesQuery<Account>());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get account by unique record ID")]
  public async Task<ActionResult<AccountDto>> GetAccountById(int id)
  {
    var result = await _mediator.Send(new GetEntityByIdQuery<Account>(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("provider/{providerId}/{accountId}")]
  public async Task<ActionResult<List<AccountDto>>> FindAccountByProviderId(int providerId, int accountId)
  {
    var result = await _mediator.Send(new FindAccountByProviderIdQuery(providerId, accountId));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("user/{userId}")]
  [SwaggerOperation(Summary = "Get all accounts for a specific user")]
  public async Task<ActionResult<List<AccountDto>>> FindAccountsByUserId(int userId)
  {
    var result = await _mediator.Send(new FindAccountsByUserIdQuery(userId));
    return (result == null) ? NotFound() : Ok(result);
  }
}
