using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands.CreateUser;

public record CreateUserCommand(
  string Email,
  string? Password,
  string Role,
  string Name,
  int? CompanyId
) : IRequest<UserDto>;
