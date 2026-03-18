using System;
using FluentValidation;

namespace Application.Commands.AccountCommands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
  public CreateAccountCommandValidator()
  {
    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.ProviderId)
        .NotEmpty()
        .WithMessage("Provider identifier is required.")
        .MaximumLength(50)
        .WithMessage("Provider identifier cannot exceed 50 characters.");

    RuleFor(command => command.Password)
        .NotEmpty()
        .WithMessage("Password is required.")
        .MinimumLength(8)
        .WithMessage("Password must be at least 8 characters long.");

    RuleFor(command => command.AccessToken)
        .MaximumLength(2048)
        .WithMessage("Access token is too long.");

    RuleFor(command => command.RefreshToken)
        .MaximumLength(2048)
        .WithMessage("Refresh token is too long.");

    RuleFor(command => command.AccessTokenExpiresAt)
        .GreaterThan(DateTime.UtcNow)
        .When(command => command.AccessTokenExpiresAt.HasValue)
        .WithMessage("Access token expiration date must be in the future.");

    RuleFor(command => command.RefreshTokenExpiresAt)
        .GreaterThan(DateTime.UtcNow)
        .When(command => command.RefreshTokenExpiresAt.HasValue)
        .WithMessage("Refresh token expiration date must be in the future.");
  }
}
