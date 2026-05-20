using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dtos;
using Application.Commands.UserCommands.CreateUser;
using Application.Commands.UserCommands.DeleteUser;
using Application.Commands.UserCommands.UpdateUser;
using Application.Queries.UserQueries.GetAllUsers;
using Application.Queries.UserQueries.GetUserById;
using Application.Queries.UserQueries.FindByRole;
using Application.Queries.UserQueries.SetUserRole;
using Swashbuckle.AspNetCore.Annotations;
using Application.Queries.UserQueries.LoginUser;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public class UserController : BaseController
{
  private readonly Domain.Abstractions.IUserRepository _userRepository;
  public UserController(IMediator mediator, Domain.Abstractions.IUserRepository userRepository) : base(mediator) { _userRepository = userRepository; }

  [HttpPost]
  [SwaggerOperation(Summary = "Create a new user")]
  [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
  public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
  {
    var result = await _mediator.Send(command);
    return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
  }

  [HttpPut("{id}")]
  [SwaggerOperation(Summary = "Update user information")]
  public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
  {
    var result = await _mediator.Send(command with { UserId = id });
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpDelete("{id}")]
  [SwaggerOperation(Summary = "Delete user")]
  public async Task<IActionResult> Delete(int id)
  {
    var result = await _mediator.Send(new DeleteUserCommand(id));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet]
  [SwaggerOperation(Summary = "Get all users")]
  public async Task<ActionResult<List<UserDto>>> GetAllUsers()
  {
    var result = await _mediator.Send(new GetAllUsersQuery());
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpGet("{id}")]
  [SwaggerOperation(Summary = "Get user by unique ID")]
  public async Task<ActionResult<UserDto>> GetUserById(int id)
  {
    var result = await _mediator.Send(new GetUserByIdQuery(id));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("role/{role}")]
  [SwaggerOperation(Summary = "Get all users with a specific role")]
  public async Task<ActionResult<List<UserDto>>> FindByRole(string role)
  {
    var result = await _mediator.Send(new FindByRoleQuery(role));
    return (result == null) ? NotFound() : Ok(result);
  }

  [HttpPost("login")]
  [SwaggerOperation(Summary = "Authenticate user and get information")]
  public async Task<IActionResult> Login([FromBody] LoginUserQuery query)
  {
    var result = await _mediator.Send(query);
    if (result == null) return NotFound();
    if (result.IsBlocked) return StatusCode(423, new { message = result.Message ?? "Account blocked" });
    return Ok(result.User);
  }

  [HttpPatch("{id}/role")]
  [SwaggerOperation(Summary = "Update user role")]
  public async Task<ActionResult<UserDto>> SetUserRole(int id, [FromBody] string role)
  {
    var result = await _mediator.Send(new SetUserRoleQuery(id, role));
    return result == null ? NotFound() : Ok(result);
  }

  [HttpGet("blocked")]
  [SwaggerOperation(Summary = "Get all blocked users")]
  public async Task<IActionResult> GetBlocked()
  {
    var result = await _userRepository.GetBlockedUsersAsync();
    return Ok(result);
  }

  [HttpPost("{id}/unblock")]
  [SwaggerOperation(Summary = "Unblock user by admin")]
  public async Task<IActionResult> Unblock(int id, [FromBody] int adminId)
  {
    var ok = await _userRepository.UnblockUserAsync(id, adminId);
    return ok ? Ok() : NotFound();
  }
}
