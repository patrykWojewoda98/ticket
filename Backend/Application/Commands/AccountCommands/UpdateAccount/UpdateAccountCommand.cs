using System;
using Domain.Entities;
using MediatR;

namespace Application.Commands.AccountCommands.UpdateAccount;

public record UpdateAccountCommand<T>(Account Account) : IRequest<Unit>;

