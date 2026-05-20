using System;
using FluentValidation;

namespace Application.Commands.TicketStatusCommands.UpdateTicketStatus;

public class UpdateTicketStatusCommandValidator : AbstractValidator<UpdateTicketStatusCommand>
{
  public UpdateTicketStatusCommandValidator()
  {
    RuleFor(command => command.TicketStatusId)
        .NotEmpty()
        .WithMessage("Status identifier is required.")
        .GreaterThan(0)
        .WithMessage("Status identifier must be a positive integer.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .When(command => command.Name != null)
        .WithMessage("Status name cannot be empty.")
        .MinimumLength(2)
        .When(command => command.Name != null)
        .WithMessage("Status name must be at least 2 characters long.")
        .MaximumLength(50)
        .When(command => command.Name != null)
        .WithMessage("Status name cannot exceed 50 characters.");
  }
}
