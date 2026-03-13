using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.AccountCommands.UpdateAccount;

public record UpdateAccountCommand(int AccountId) : IRequest<AccountDto>;

