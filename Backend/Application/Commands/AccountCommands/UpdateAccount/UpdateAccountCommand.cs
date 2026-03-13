using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AccountCommands.UpdateAccount;

public record UpdateAccountCommand(Account Account) : IRequest<Unit>;

