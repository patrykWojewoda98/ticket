using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.UpdateUser;

public record UpdateUserCommand(User User) : IRequest<Unit>;
