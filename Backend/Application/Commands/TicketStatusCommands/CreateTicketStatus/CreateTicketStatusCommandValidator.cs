using System;
using FluentValidation;

namespace Application.Commands.TicketStatusCommands.CreateTicketStatus;

public class CreateTicketStatusCommandValidator : AbstractValidator<CreateTicketStatusCommand>
{
  public CreateTicketStatusCommandValidator()
  {
    RuleFor(command => command.Name)
        .NotEmpty()
        .WithMessage("Status name is required.")
        .MinimumLength(2)
        .WithMessage("Status name must be at least 2 characters long.")
        .MaximumLength(50)
        .WithMessage("Status name cannot exceed 50 characters.");
  }
}
