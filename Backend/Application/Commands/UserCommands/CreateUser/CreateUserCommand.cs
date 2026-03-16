using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands.CreateUser;

public record CreateUserCommand(
  string Email,
  bool EmailVerified,
  string Role,
  string Name,
  string? Image,
  int? CompanyId
) : IRequest<UserDto>;
