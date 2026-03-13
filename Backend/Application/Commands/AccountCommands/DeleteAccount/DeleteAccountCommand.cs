using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.AccountCommands.DeleteAccount;

public record DeleteAccountCommand(int AccountId) : IRequest<AccountDto>;
