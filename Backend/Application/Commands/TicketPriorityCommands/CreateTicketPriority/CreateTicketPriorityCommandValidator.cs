using System;
using FluentValidation;

namespace Application.Commands.TicketPriorityCommands.CreateTicketPriority;

public class CreateTicketPriorityCommandValidator : AbstractValidator<CreateTicketPriorityCommand>
{
  public CreateTicketPriorityCommandValidator()
  {
    RuleFor(command => command.Name)
        .NotEmpty()
        .WithMessage("Priority name is required.")
        .MinimumLength(3)
        .WithMessage("Priority name must be at least 3 characters long.")
        .MaximumLength(50)
        .WithMessage("Priority name cannot exceed 50 characters.");
  }
}
