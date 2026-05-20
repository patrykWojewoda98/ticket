using System;
using FluentValidation;

namespace Application.Commands.TicketHistoryCommands.UpdateTicketHistory;

public class UpdateTicketHistoryCommandValidator : AbstractValidator<UpdateTicketHistoryCommand>
{
  public UpdateTicketHistoryCommandValidator()
  {
    RuleFor(command => command.TicketId)
        .NotEmpty()
        .GreaterThan(0);

    RuleFor(command => command.UserId)
        .NotEmpty()
        .GreaterThan(0);

    RuleFor(command => command.Action)
        .NotEmpty()
        .When(command => command.Action != null)
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
