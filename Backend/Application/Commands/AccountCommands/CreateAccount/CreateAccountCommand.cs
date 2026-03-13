using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AccountCommands.CreateAccount;

public record CreateAccountCommand(Account Account) : IRequest<Unit>;
