using System;
using FluentValidation;

namespace Application.Commands.TicketHistoryCommands.CreateTicketHistory;

public class CreateTicketHistoryCommandValidator : AbstractValidator<CreateTicketHistoryCommand>
{
  public CreateTicketHistoryCommandValidator()
  {
    RuleFor(command => command.TicketId)
        .NotEmpty()
        .WithMessage("Ticket identifier is required.")
        .GreaterThan(0)
        .WithMessage("Ticket identifier must be a positive integer.");

    RuleFor(command => command.UserId)
        .NotEmpty()
        .WithMessage("User identifier is required.")
        .GreaterThan(0)
        .WithMessage("User identifier must be a positive integer.");

    RuleFor(command => command.Action)
        .NotEmpty()
        .WithMessage("Action description is required.")
        .MaximumLength(100)
        .WithMessage("Action description cannot exceed 100 characters.");

    RuleFor(command => command.OldValue)
        .MaximumLength(1000)
        .When(command => command.OldValue != null)
        .WithMessage("Old value content is too long.");

    RuleFor(command => command.NewValue)
        .MaximumLength(1000)
        .When(command => command.NewValue != null)
        .WithMessage("New value content is too long.");
  }
}
