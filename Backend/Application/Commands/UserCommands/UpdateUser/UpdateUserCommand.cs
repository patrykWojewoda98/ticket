using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands.UpdateUser;

public record UpdateUserCommand(
  int UserId,
  string? Email,
  string? Password,
  string? Role,
  string? Name,
  int? CompanyId
) : IRequest<UserDto>;
