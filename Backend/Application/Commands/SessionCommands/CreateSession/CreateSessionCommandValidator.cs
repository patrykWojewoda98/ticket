using System;
using FluentValidation;

namespace Application.Commands.SessionCommands.CreateSession;

public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
{
  public CreateSessionCommandValidator()
  {
    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.Token)
        .NotEmpty()
        .WithMessage("Session token is required.");

    RuleFor(command => command.ExpiresAt)
        .NotEmpty()
        .WithMessage("Expiration date is required.")
        .GreaterThan(DateTime.UtcNow)
        .WithMessage("Session expiration date must be in the future.");

    RuleFor(command => command.IpAddress)
        .MaximumLength(45)
        .WithMessage("IP address is too long.");

    RuleFor(command => command.UserAgent)
        .MaximumLength(500)
        .WithMessage("User agent string is too long.");
  }
}
