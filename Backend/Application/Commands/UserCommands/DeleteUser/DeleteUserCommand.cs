using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands.DeleteUser;

public record DeleteUserCommand(
  int UserId
) : IRequest<UserDto>;
