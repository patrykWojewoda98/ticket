using System;
using FluentValidation;

namespace Application.Commands.TicketPriorityCommands.UpdateTicketPriority;

public class UpdateTicketPriorityCommandValidator : AbstractValidator<UpdateTicketPriorityCommand>
{
  public UpdateTicketPriorityCommandValidator()
  {
    RuleFor(command => command.TicketPriorityId)
        .NotEmpty()
        .WithMessage("Priority identifier is required.")
        .GreaterThan(0)
        .WithMessage("Priority identifier must be a positive integer.");

    RuleFor(command => command.Name)
        .NotEmpty()
        .When(command => command.Name != null)
        .WithMessage("Priority name cannot be empty.")
        .MinimumLength(3)
        .When(command => command.Name != null)
        .WithMessage("Priority name must be at least 3 characters long.")
        .MaximumLength(50)
        .When(command => command.Name != null)
        .WithMessage("Priority name cannot exceed 50 characters.");
  }
}
