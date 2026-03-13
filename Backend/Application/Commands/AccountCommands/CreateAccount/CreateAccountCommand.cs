using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.AccountCommands.CreateAccount;

public record CreateAccountCommand(int UserId, int ProviderId) : IRequest<AccountDto>;
