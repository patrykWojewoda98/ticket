using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.DeleteUser;

public record DeleteUserCommand(User User) : IRequest<Unit>;
