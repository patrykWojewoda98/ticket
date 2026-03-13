using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.UserCommands.CreateUser;

public record CreateUserCommand(User User) : IRequest<Unit>;
