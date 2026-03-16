using System;
using FluentValidation;

namespace Application.Commands.SessionCommands.UpdateSession;

public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
{
  public UpdateSessionCommandValidator()
  {
    RuleFor(command => command.SessionId)
        .NotEmpty()
        .WithMessage("Session identifier is required.")
        .GreaterThan(0)
        .WithMessage("Session identifier must be a positive integer.");

    RuleFor(command => command.Token)
        .NotEmpty()
        .When(command => command.Token != null)
        .WithMessage("Session token cannot be empty.");

    RuleFor(command => command.ExpiresAt)
        .GreaterThan(DateTime.UtcNow)
        .When(command => command.ExpiresAt.HasValue)
        .WithMessage("Session expiration date must be in the future.");

    RuleFor(command => command.IpAddress)
        .MaximumLength(45)
        .When(command => command.IpAddress != null)
        .WithMessage("IP address is too long.");

    RuleFor(command => command.UserAgent)
        .MaximumLength(500)
        .When(command => command.UserAgent != null)
        .WithMessage("User agent string is too long.");
  }
}
