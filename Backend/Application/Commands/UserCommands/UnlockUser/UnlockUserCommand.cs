using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.UserCommands.UnlockUser;

public record UnlockUserCommand(int UserId) : IRequest<UserDto?>;
