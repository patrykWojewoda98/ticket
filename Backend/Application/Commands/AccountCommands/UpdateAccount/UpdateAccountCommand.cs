using System;
using Application.Dtos;
using MediatR;

namespace Application.Commands.AccountCommands.UpdateAccount;

public record UpdateAccountCommand(
    int AccountId,
    int? UserId,
    int? ProviderId,
    string? Password,
    string? Scope,
    string? IdToken,
    string? AccessToken,
    string? RefreshToken,
    DateTime? AccessTokenExpiresAt,
    DateTime? RefreshTokenExpiresAt
) : IRequest<AccountDto>;
