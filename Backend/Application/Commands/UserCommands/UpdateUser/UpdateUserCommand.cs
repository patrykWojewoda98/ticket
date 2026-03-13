using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands.UpdateUser;

public record UpdateUserCommand(
  int UserId,
  string? Email,
  bool? EmailVerified,
  string? Role,
  string? Name,
  string? Image,
  int? CompanyId
) : IRequest<UserDto>;
