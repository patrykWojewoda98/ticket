using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AccountCommands.DeleteAccount;

public record DeleteAccountCommand(Account Account) : IRequest<Unit>;
