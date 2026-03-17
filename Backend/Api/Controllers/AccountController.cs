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

namespace Api.Controllers;

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
    // Zmienione na nameof(GetByUserId) i przekazujemy userId, bo to Twoja jedyna metoda GET
    return CreatedAtAction(nameof(GetByUserId), new { userId = result.UserId }, result);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateAccountCommand command)
  {
    var result = await _mediator.Send(command with { AccountId = id });
    if (result == null) return NotFound();
    return Ok(result);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteAccountCommand(id));
    if (result == null) return NotFound();
    return NoContent();
  }

  // Usunąłem GetAll, bo wcześniej wywalało błąd rejestracji MediatR (generyki).
  // Jeśli go potrzebujesz, musisz stworzyć dedykowane GetAccountsQuery.

  [HttpGet("user/{userId}")] // Zmieniłem trasę na bardziej logiczną
  [SwaggerOperation(Summary = "Get accounts by User ID")]
  [ProducesResponseType(typeof(List<AccountDto>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<List<AccountDto>>> GetByUserId(int userId)
  {
    // Używamy Twojego nowego, sprawnego Handlera
    var result = await _mediator.Send(new FindAccountsByUserIdQuery(userId));

    if (result == null || !result.Any()) return NotFound();

    return Ok(result);
  }
}
