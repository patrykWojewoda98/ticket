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
    // Po stworzeniu przekierowujemy do GetByUserId, bo to masz zaimplementowane
    return CreatedAtAction(nameof(GetByUserId), new { userId = result.UserId }, result);
  }

  // ... Update i Delete bez zmian ...

  [HttpGet("user/{userId}")] // Zmiana trasy, aby pasowała do FindAccountsByUserIdQuery
  [SwaggerOperation(Summary = "Get accounts by User ID")]
  [ProducesResponseType(typeof(List<AccountDto>), (int)HttpStatusCode.OK)]
  public async Task<ActionResult<List<AccountDto>>> GetByUserId(int userId)
  {
    // UŻYWAMY TWOJEGO NOWEGO HANDLERA
    var result = await _mediator.Send(new FindAccountsByUserIdQuery(userId));

    if (result == null || !result.Any()) return NotFound();

    return Ok(result);
  }
}
